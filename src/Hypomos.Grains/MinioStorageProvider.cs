namespace Hypomos.Grains
{
    using System;
    using System.Threading.Tasks;
    using Humanizer;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;

    using Minio;
    using Orleans;

    public class MinioStorageProvider : Grain, IMinioStorageProvider
    {
        private MinioConfiguration config;

        public Task SetConfiguration(MinioConfiguration config)
        {
            this.config = config;

            return Task.CompletedTask;
        }

        public async Task Scan()
        {
            var minio = new MinioClient(this.config.Endpoint,
                this.config.AccessKey,
                this.config.SecretKey
            );

            if (this.config.WithSsl)
            {
                minio = minio.WithSSL();
            }

            if (await minio.BucketExistsAsync(this.config.BucketName))
            {
                var observable = minio.ListObjectsAsync(this.config.BucketName, recursive: true);
                var disposable = observable.Subscribe(item =>
                    {
                        Console.WriteLine($"Object: {item.Key}, {item.ETag}, {item.LastModifiedDateTime}, {((long)item.Size).Bytes()}");
                    },
                    ex => Console.WriteLine($"OnError: {ex}"),
                    () => Console.WriteLine($"Listed all objects in bucket {this.config.BucketName}\n"));

            }

            // var buckets = await minio.ListBucketsAsync();
        }
    }
}