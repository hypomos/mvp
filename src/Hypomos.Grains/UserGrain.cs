namespace Hypomos.Grains
{
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
        public Task<List<StorageConfiguration>> GetStorageConfigurations()
        {
            return Task.FromResult(new List<StorageConfiguration>());
        }

        public Task SetStorageConfigurations(List<StorageConfiguration> configurations)
        {
            this.State.SetupState.AreStorageSourcesSet = true;

            return Task.CompletedTask;
        }

        public Task<UserSetupState> GetSetupStateAsync()
        {
            return Task.FromResult(new UserSetupState
            {
                ArePersonalDetailsSet = this.State.SetupState.ArePersonalDetailsSet,
                AreStorageSourcesSet = this.State.SetupState.AreStorageSourcesSet
            });
        }

        public Task<UserData> GetPersonalDetails()
        {
            return Task.FromResult(new UserData
            {
                Username = this.State.Username,
                GivenName = this.State.GivenName,
                Surname = this.State.Surname,
                EmailAddress = this.State.EmailAddress
            });
        }

        public async Task SetPersonalDetails(UserPersonalDetails personalDetails)
        {
            this.State.SetupState.ArePersonalDetailsSet = true;

            this.State.Username = personalDetails.Username;
            this.State.EmailAddress = personalDetails.EmailAddress;
            this.State.Surname = personalDetails.Surname;
            this.State.GivenName = personalDetails.GivenName;

            await this.WriteStateAsync();
        }

        public Task<IEnumerable<IStorageProvider>> GetStorageProviders()
        {
            throw new System.NotImplementedException();
        }

        public Task AddStorageProvider(StorageConfiguration configuration)
        {
            throw new System.NotImplementedException();
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

        public override async Task OnActivateAsync()
        {
            if (this.State == null)
            {
                this.State = new UserState();
            }

            if (this.State.SetupState == null)
            {
                this.State.SetupState = new UserSetupState();
            }

            if (this.State.MediaLibraries == null)
            {
                this.State.MediaLibraries = new List<IMediaLibrary>();
            }

            await this.WriteStateAsync();
            await base.OnActivateAsync();
        }
    }
}