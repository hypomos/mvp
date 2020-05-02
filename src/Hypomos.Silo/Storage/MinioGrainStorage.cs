// Taken from: https://github.com/OrleansContrib/Orleans.Persistence.Minio
// MIT License

using BucketNotFoundException = Minio.Exceptions.BucketNotFoundException;
using ObjectNotFoundException = Minio.Exceptions.ObjectNotFoundException;

namespace Hypomos.Silo.Storage
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Orleans;
    using Orleans.Runtime;
    using Orleans.Serialization;
    using Orleans.Storage;

    public class MinioGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
    {
        private readonly string _container;
        private readonly IGrainFactory _grainFactory;
        private readonly ILogger<MinioGrainStorage> _logger;

        private readonly string _name;
        private readonly IMinioStorage _storage;
        private readonly ITypeResolver _typeResolver;
        private JsonSerializerSettings _jsonSettings;

        public MinioGrainStorage(string name, string container, IMinioStorage storage,
            ILogger<MinioGrainStorage> logger, IGrainFactory grainFactory, ITypeResolver typeResolver)
        {
            this._name = name;
            this._container = container;
            this._logger = logger;
            this._storage = storage;
            this._grainFactory = grainFactory;
            this._typeResolver = typeResolver;
        }

        public async Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var blobName = this.GetBlobNameString(grainType, grainReference);

            try
            {
                this._logger.LogTrace("Clearing: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4}",
                    grainType, grainReference, grainState.ETag, blobName, this._container);

                await this._storage.DeleteBlob(this._container, blobName);
                grainState.ETag = null;

                this._logger.LogTrace("Cleared: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4}",
                    grainType, grainReference, grainState.ETag, blobName, this._container);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex,
                    "Error clearing: GrainType={0} Grainid={1} ETag={2} BlobName={3} in Container={4} Exception={5}",
                    grainType, grainReference, grainState.ETag, blobName, this._container, ex.Message);

                throw;
            }
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var blobName = this.GetBlobNameString(grainType, grainReference);

            try
            {
                this._logger.LogTrace("Reading: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4}",
                    grainType, grainReference, grainState.ETag, blobName, this._container);

                GrainStateRecord record;
                try
                {
                    using (var blob = await this._storage.ReadBlob(this._container, blobName))
                    using (var stream = new MemoryStream())
                    {
                        await blob.CopyToAsync(stream);
                        record = this.ConvertFromStorageFormat(stream.ToArray());
                    }
                }
                catch (BucketNotFoundException ex)
                {
                    this._logger.LogTrace(
                        "ContainerNotFound reading: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4} Exception={5}",
                        grainType, grainReference, grainState.ETag, blobName, this._container, ex.message);

                    return;
                }
                catch (ObjectNotFoundException ex)
                {
                    this._logger.LogTrace(
                        "BlobNotFound reading: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4} Exception={5}",
                        grainType, grainReference, grainState.ETag, blobName, this._container, ex.message);

                    return;
                }

                grainState.State = record.State;
                grainState.ETag = record.ETag.ToString();

                this._logger.LogTrace("Read: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4}",
                    grainType, grainReference, grainState.ETag, blobName, this._container);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex,
                    "Error reading: GrainType={0} Grainid={1} ETag={2} from BlobName={3} in Container={4} Exception={5}",
                    grainType, grainReference, grainState.ETag, blobName, this._container, ex.Message);

                throw;
            }
        }

        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var blobName = this.GetBlobNameString(grainType, grainReference);

            var newETag = string.IsNullOrEmpty(grainState.ETag) ? 0 : int.Parse(grainState.ETag) + 1;
            try
            {
                this._logger.LogTrace("Writing: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4}",
                    grainType, grainReference, grainState.ETag, blobName, this._container);

                var record = new GrainStateRecord
                {
                    ETag = newETag,
                    State = grainState.State
                };

                using (var stream = new MemoryStream(this.ConvertToStorageFormat(record)))
                {
                    await this._storage.UploadBlob(this._container, blobName, stream, contentType: "application/json");
                }

                grainState.ETag = newETag.ToString();

                this._logger.LogTrace("Wrote: GrainType={0} Grainid={1} ETag={2} to BlobName={3} in Container={4}",
                    grainType, grainReference, grainState.ETag, blobName, this._container);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex,
                    "Error writing: GrainType={0} Grainid={1} ETag={2} from BlobName={3} in Container={4} Exception={5}",
                    grainType, grainReference, grainState.ETag, blobName, this._container, ex.Message);

                throw;
            }
        }

        public void Participate(ISiloLifecycle lifecycle)
        {
            lifecycle.Subscribe(OptionFormattingUtilities.Name<MinioGrainStorage>(this._name),
                ServiceLifecycleStage.ApplicationServices, this.Init);
        }

        private string GetBlobNameString(string grainType, GrainReference grainReference)
        {
            return $"{grainType}-{grainReference.ToKeyString()}";
        }

        private byte[] ConvertToStorageFormat(object record)
        {
            var data = JsonConvert.SerializeObject(record, this._jsonSettings);
            return Encoding.UTF8.GetBytes(data);
        }

        private GrainStateRecord ConvertFromStorageFormat(byte[] content)
        {
            var json = Encoding.UTF8.GetString(content);
            var record = JsonConvert.DeserializeObject<GrainStateRecord>(json, this._jsonSettings);
            return record;
        }

        private async Task Init(CancellationToken ct)
        {
            this._jsonSettings = OrleansJsonSerializer.UpdateSerializerSettings(
                OrleansJsonSerializer.GetDefaultSerializerSettings(this._typeResolver, this._grainFactory), false,
                false, null);

            if (!await this._storage.ContainerExits(this._container))
            {
                await this._storage.CreateContainerAsync(this._container);
            }
        }

        public class GrainStateRecord
        {
            public int ETag { get; set; }
            public object State { get; set; }
        }
    }
}