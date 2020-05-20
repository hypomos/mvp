namespace Hypomos.Grains
{
    using System;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Humanizer;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;
    using Minio;
    using Orleans;

    public class MinioStorageProviderGrain : Grain, IMinioStorageProviderGrain
    {
        private MinioConfiguration config;

        public Task SetConfiguration(MinioConfiguration config)
        {
            this.config = config;

            return Task.CompletedTask;
        }

        public async Task Scan(string username)
        {
            var minio = new MinioClient(this.config.Endpoint,
                this.config.AccessKey,
                this.config.SecretKey);

            if (this.config.WithSsl)
            {
                minio = minio.WithSSL();
            }

            if (await minio.BucketExistsAsync(this.config.BucketName))
            {
                var mediaLibrary =
                    this.GrainFactory.GetGrain<IMediaLibrary>(
                        $"{username}@{this.config.Endpoint}/{this.config.BucketName}");

                var observable = minio.ListObjectsAsync(this.config.BucketName, recursive: true);

                var streamProvider = this.GetStreamProvider(Constants.SmsProvider);
                var scanEventStream = streamProvider.GetStream<ScanEventData>(this.GetPrimaryKey(), username);

                var disposable = observable
                    .SubscribeOn(SynchronizationContext.Current)
                    .Subscribe(item =>
                    {
                        var mediaItem =
                            this.GrainFactory.GetGrain<IMediaItem>(
                                $"{this.config.Endpoint}/{this.config.BucketName}/{item.Key}");

                        mediaLibrary.AddMediaItem(mediaItem);

                        scanEventStream.OnNextAsync(new ScanEventData
                        {
                            ETag = item.ETag,
                            IsDir = item.IsDir,
                            Key = item.Key,
                            LastModifiedDateTime = item.LastModifiedDateTime,
                            LastModified = item.LastModified
                        });

                        Console.WriteLine(
                            $"Object: {item.Key}, {item.ETag}, {item.LastModifiedDateTime}, {((long) item.Size).Bytes()}");
                    },
                    ex =>
                    {
                        scanEventStream.OnErrorAsync(ex);
                        Console.WriteLine($"OnError: {ex}");
                    },
                    () =>
                    {
                        scanEventStream.OnCompletedAsync();
                        Console.WriteLine($"Listed all objects in bucket {this.config.BucketName}\n");
                    });
            }
        }
    }
}