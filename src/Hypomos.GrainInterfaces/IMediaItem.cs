namespace Hypomos.GrainInterfaces
{
    using System.Threading.Tasks;

    using Hypomos.GrainInterfaces.Models;

    using Orleans;

    /// <summary>
    ///     A media item must know:
    ///     - if it's a picture or a video
    ///     - where it's stored (storage Provider), so that it can load it's state from there
    ///     - must be able to have labels (or should I handle this in external system?)
    ///     - be able to query embedded attributes (Exif etc.)
    /// </summary>
    public interface IMediaItem : IGrainWithStringKey
    {
        Task<MetaData> GetMetaData();
    }
}