namespace Hypomos.Grains
{
    using System.Collections.Generic;

    using Hypomos.GrainInterfaces;
    using Hypomos.GrainInterfaces.Models;

    public class MediaLibraryState
    {
        public MediaLibraryMeta Meta { get; set; }
        public List<IMediaItem> Items { get; set; }
    }
}