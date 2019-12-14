namespace Hypomos.Grains
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;

    using Orleans;
    using Orleans.Providers;

    [StorageProvider(ProviderName = "minio-orleans")]
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

        public Task<bool> IsInitialized()
        {
            return Task.FromResult(this.State.IsInitialized);
        }

        public async Task Initialize(UserInitializationContext initializationContext)
        {
            this.State.IsInitialized = true;

            this.State.Username = initializationContext.Username;
            this.State.EmailAddress = initializationContext.EmailAddress;
            this.State.Surname = initializationContext.Surname;
            this.State.GivenName = initializationContext.GivenName;

            await this.WriteStateAsync();
        }
    }

    public class UserState
    {
        public bool IsInitialized { get; set; }

        public string Username { get; set; }
        
        public string EmailAddress { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }
}
