namespace Hypomos.Grains
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            throw new NotImplementedException();
        }

        public Task SetStorageConfigurations(List<StorageConfiguration> configurations)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInitialized()
        {
            return Task.FromResult(this.State.IsInitialized);
        }

        public Task<UserData> GetState()
        {
            return Task.FromResult(new UserData
            {
                Username = this.State.Username,
                GivenName = this.State.GivenName,
                IsInitialized = this.State.IsInitialized,
                Surname = this.State.Surname,
                EmailAddress = this.State.EmailAddress
            });
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

        public Task AddMediaLibrary(IMediaLibrary mediaLibrary)
        {
            this.State.MediaLibraries.Add(mediaLibrary);
            return Task.CompletedTask;
        }

        public Task<IMediaLibrary> GetMediaLibrary(string key)
        {
            var result = this.State.MediaLibraries.FirstOrDefault(ml => ml.GetPrimaryKeyString() == key);
            return Task.FromResult(result);
        }

        public override Task OnActivateAsync()
        {
            if (this.State == null)
            {
                this.State = new UserState();
            }

            if (this.State.MediaLibraries == null)
            {
                this.State.MediaLibraries = new List<IMediaLibrary>();
            }

            return base.OnActivateAsync();
        }
    }
}