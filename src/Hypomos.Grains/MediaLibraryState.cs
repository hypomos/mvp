namespace Hypomos.Grains
{
    using System.Collections.Generic;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;

    public class MediaLibraryState
    {
        public MediaLibraryMeta Meta { get; set; }
        public List<IMediaItem> Items { get; set; }
    }
}