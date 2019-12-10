namespace Hypomos.Interfaces
{
    using System.Threading.Tasks;
    using Orleans;

    /// <summary>
    /// A media item must know:
    /// - if it's a picture or a video
    /// - where it's stored (storage Provider), so that it can load it's state from there
    /// - must be able to have labels (or should I handle this in external system?)
    /// - be able to query embedded attributes (Exif etc.)
    /// </summary>
    public interface IMediaItem : IGrainWithGuidKey
    {
        Task<MetaData> GetMetaData();
    }
}