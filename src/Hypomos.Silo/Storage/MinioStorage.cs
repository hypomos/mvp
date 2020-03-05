// Taken from: https://github.com/OrleansContrib/Orleans.Persistence.Minio
// MIT License

using MinioClient = Minio.MinioClient;

namespace Orleans.Persistence.Minio.Storage
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class MinioStorage : IMinioStorage
    {
        private readonly string _accessKey;
        private readonly string _containerPrefix;
        private readonly string _endpoint;
        private readonly ILogger<MinioStorage> _logger;
        private readonly string _secretKey;
        private readonly Stopwatch stopwwatch = new Stopwatch();

        public MinioStorage(ILogger<MinioStorage> logger, MinioGrainStorageOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.AccessKey))
            {
                throw new ArgumentException("Minio 'accessKey' is missing.");
            }

            if (string.IsNullOrWhiteSpace(options.SecretKey))
            {
                throw new ArgumentException("Minio 'secretKey' is missing.");
            }

            if (string.IsNullOrWhiteSpace(options.Endpoint))
            {
                throw new ArgumentException("Minio 'endpoint' is missing.");
            }

            this._accessKey = options.AccessKey;
            this._secretKey = options.SecretKey;
            this._endpoint = options.Endpoint;
            this._logger = logger;
        }

        public MinioStorage(ILogger<MinioStorage> logger, string accessKey, string secretKey, string endpoint,
            string containerPrefix)
            : this(logger, new MinioGrainStorageOptions
            {
                AccessKey = accessKey,
                SecretKey = secretKey,
                Endpoint = endpoint
            })
        {
            if (string.IsNullOrWhiteSpace(containerPrefix))
            {
                throw new ArgumentException("Minio 'containerPrefix' is missing.");
            }

            this._containerPrefix = containerPrefix;
        }

        public Task<bool> ContainerExits(string blobContainer)
        {
            return this.CreateMinioClient().BucketExistsAsync(this.AppendContainerPrefix(blobContainer));
        }

        public Task CreateContainerAsync(string blobContainer)
        {
            return this.CreateMinioClient().MakeBucketAsync(blobContainer);
        }

        public async Task DeleteBlob(string blobContainer, string blobName, string blobPrefix = null)
        {
            var (client, bucket, objectName) = this.GetStorage(blobContainer, blobPrefix, blobName);

            this._logger.LogTrace("Deleting blob: container={0} blobName={1} blobPrefix={2}", blobContainer, blobName,
                blobPrefix);
            this.stopwwatch.Restart();

            await client.RemoveObjectAsync(bucket, objectName);

            this.stopwwatch.Stop();
            this._logger.LogTrace("Deleted blob: timems={0} container={0} blobName={1} blobPrefix={2}",
                this.stopwwatch.ElapsedMilliseconds, blobContainer, blobName, blobPrefix);
        }

        public async Task<Stream> ReadBlob(string blobContainer, string blobName, string blobPrefix = null)
        {
            var (client, bucket, objectName) = this.GetStorage(blobContainer, blobPrefix, blobName);

            this._logger.LogTrace("Reading blob: container={0} blobName={1} blobPrefix={2}", blobContainer, blobName,
                blobPrefix);
            this.stopwwatch.Restart();

            var ms = new MemoryStream();
            await client.GetObjectAsync(bucket, objectName, stream => { stream.CopyTo(ms); });

            this.stopwwatch.Stop();
            this._logger.LogTrace("Read blob: timems={0} container={0} blobName={1} blobPrefix={2}",
                this.stopwwatch.ElapsedMilliseconds, blobContainer, blobName, blobPrefix);

            ms.Position = 0;
            return ms;
        }

        public async Task UploadBlob(string blobContainer, string blobName, Stream blob, string blobPrefix = null,
            string contentType = null)
        {
            var (client, container, name) = this.GetStorage(blobContainer, blobPrefix, blobName);

            this._logger.LogTrace("Writing blob: container={0} blobName={1} blobPrefix={2}", blobContainer, blobName,
                blobPrefix);
            this.stopwwatch.Restart();

            await client.PutObjectAsync(container, name, blob, blob.Length, contentType);
            this.stopwwatch.Stop();
            this._logger.LogTrace("Wrote blob: timems={0} container={0} blobName={1} blobPrefix={2}",
                this.stopwwatch.ElapsedMilliseconds, blobContainer, blobName, blobPrefix);
        }

        private MinioClient CreateMinioClient()
        {
            return new MinioClient(this._endpoint, this._accessKey, this._secretKey);
        }

        private string AppendPrefix(string prefix, string value)
        {
            return string.IsNullOrEmpty(prefix) ? value : $"{prefix}-{value}";
        }

        private string AppendContainerPrefix(string container)
        {
            return string.IsNullOrEmpty(this._containerPrefix)
                ? container
                : this.AppendPrefix(this._containerPrefix, container);
        }

        private (MinioClient client, string bucket, string objectName) GetStorage(string blobContainer,
            string blobPrefix, string blobName)
        {
            this._logger.LogTrace("Creating Minio client: container={0} blobName={1} blobPrefix={2}", blobContainer,
                blobName, blobPrefix);
            this.stopwwatch.Restart();

            var client = this.CreateMinioClient();

            this.stopwwatch.Stop();
            this._logger.LogTrace("Created Minio client: timems={0} container={0} blobName={1} blobPrefix={2}",
                this.stopwwatch.ElapsedMilliseconds, blobContainer, blobName, blobPrefix);

            return (client, this.AppendContainerPrefix(blobContainer), this.AppendPrefix(blobPrefix, blobName));
        }
    }
}