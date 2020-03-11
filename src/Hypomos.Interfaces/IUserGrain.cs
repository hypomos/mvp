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
        Task<List<StorageConfiguration>> GetStorageConfigurations();

        Task SetStorageConfigurations(List<StorageConfiguration> configurations);

        Task<UserSetupState> GetSetupStateAsync();
     
        Task<UserData> GetPersonalDetails();

        Task SetPersonalDetails(UserPersonalDetails personalDetails);

        Task AddMediaLibrary(IMediaLibrary mediaLibrary);

        Task<IMediaLibrary> GetMediaLibrary(string key);
    }
}