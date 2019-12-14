namespace Hypomos.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Hypomos.Interfaces.Models;

    using Orleans;

    /// <summary>
    /// key = identifier
    /// </summary>
    public interface IUserGrain : IGrainWithStringKey
    {
        Task<string> GetUsername();

        Task<List<StorageConfiguration>> GetStorageConfigurations();

        Task SetStorageConfigurations(List<StorageConfiguration> configurations);
        
        Task<bool> IsInitialized();

        Task Initialize(UserInitializationContext initializationContext);
    }

    public class UserInitializationContext
    {
        public string Username { get; set; }

        public string EmailAddress { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }
}