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
    }
}