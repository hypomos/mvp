namespace Hypomos.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Orleans;

    public interface IMediaLibrary : IGrainWithStringKey
    {
        Task<MediaLibraryMeta> GetMeta();

        Task SetMeta(MediaLibraryMeta meta);

        Task<IUserGrain> GetCreator();

        Task<string> GetName();

        Task AddMediaItem(IMediaItem item);

        Task RemoveMediaItem(IMediaItem item);

        Task<List<IMediaItem>> QueryMediaItems(Func<IMediaItem, bool> selector);
    }
}