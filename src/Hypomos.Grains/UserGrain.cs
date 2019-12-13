namespace Hypomos.Grains
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;

    using Orleans;

    public class UserGrain : Grain<UserState>, IUserGrain
    {
        public Task<string> GetUsername()
        {
            return Task.FromResult(this.State.Username);
        }

        public Task<List<StorageConfiguration>> GetStorageConfigurations()
        {
            throw new System.NotImplementedException();
        }

        public Task SetStorageConfigurations(List<StorageConfiguration> configurations)
        {
            throw new System.NotImplementedException();
        }
    }

    public class UserState
    {
        public string Username { get; set; }
    }
}
