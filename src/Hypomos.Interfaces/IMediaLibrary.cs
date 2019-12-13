namespace Hypomos.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Orleans;

    public interface IMediaLibrary : IGrainWithGuidKey
    {
        Task<MediaLibraryMeta> GetMeta();

        Task SetMeta(MediaLibraryMeta meta);
        
        Task<IUserGrain> GetCreator();

        Task<string> GetName();

        Task AddMediaItem(IMediaItem item);
        
        Task RemoveMediaItem(IMediaItem item);

        Task<List<IMediaItem>> QueryMediaItems(Func<IMediaItem, bool> selector);
    }

    public class MediaLibraryMeta
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}