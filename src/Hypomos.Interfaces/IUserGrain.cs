namespace Hypomos.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hypomos.Interfaces.Models;
    using Orleans;

    /// <summary>
    ///     key = identifier
    /// </summary>
    public interface IUserGrain : IGrainWithStringKey
    {
        Task<UserData> GetPersonalDetails();

        Task SetPersonalDetails(UserPersonalDetails personalDetails);

        Task<IEnumerable<IStorageProvider>> GetStorageProviders();

        Task AddStorageProvider(StorageConfiguration configuration);

        //Task AddMediaLibrary(IMediaLibrary mediaLibrary);

        //Task<IMediaLibrary> GetMediaLibrary(string key);
    }
}