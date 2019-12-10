namespace Hypomos.Interfaces
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IMediaLibrary : IGrainWithGuidKey
    {
        Task<IUserGrain> GetCreator();

        Task<string> GetName();
    }
}