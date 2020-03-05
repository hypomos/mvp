namespace Hypomos.Grains
{
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;
    using Orleans;
    using Orleans.Providers;

    [StorageProvider(ProviderName = "minio-orleans")]
    public class MediaItemGrain : Grain<MetaData>, IMediaItem
    {
        public Task<MetaData> GetMetaData()
        {
            this.State.Name = this.GetPrimaryKeyString();

            return Task.FromResult(this.State);
        }
    }
}