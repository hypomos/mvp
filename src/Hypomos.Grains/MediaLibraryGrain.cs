namespace Hypomos.Grains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;
    using Orleans;
    using Orleans.Providers;

    [StorageProvider(ProviderName = "minio-orleans")]
    public class MediaLibraryGrain : Grain<MediaLibraryState>, IMediaLibrary
    {
        public Task<MediaLibraryMeta> GetMeta()
        {
            return Task.FromResult(this.State.Meta);
        }

        public Task SetMeta(MediaLibraryMeta meta)
        {
            this.State.Meta = meta;
            return Task.CompletedTask;
        }

        public Task<IUserGrain> GetCreator()
        {
            return Task.FromResult((IUserGrain) null);
        }

        public Task<string> GetName()
        {
            return Task.FromResult(string.Empty);
        }

        public Task AddMediaItem(IMediaItem item)
        {
            this.State.Items.Add(item);
            return Task.CompletedTask;
        }

        public Task RemoveMediaItem(IMediaItem item)
        {
            this.State.Items.Remove(item);
            return Task.CompletedTask;
        }

        public Task<List<IMediaItem>> QueryMediaItems(Func<IMediaItem, bool> selector)
        {
            return Task.FromResult(this.State.Items.Where(selector).ToList());
        }

        public override Task OnActivateAsync()
        {
            if (this.State == null)
            {
                this.State = new MediaLibraryState
                {
                    Items = new List<IMediaItem>()
                };
            }

            return base.OnActivateAsync();
        }
    }
}