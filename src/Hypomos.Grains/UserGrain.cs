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

        public Task<IEnumerable<StorageConfiguration>> GetStorageProviders()
        {
            var storages = this.State.Storages;
            return Task.FromResult((IEnumerable<StorageConfiguration>) storages);
        }

        public async Task AddStorageProvider(StorageConfiguration configuration)
        {
            this.State.Storages.Add(configuration);
            await this.WriteStateAsync();
        }

        public override async Task OnActivateAsync()
        {
            this.State ??= new UserState();
            this.State.SetupState ??= new UserSetupState();
            this.State.MediaLibraries ??= new List<IMediaLibrary>();
            this.State.Storages ??= new List<StorageConfiguration>();

            await this.WriteStateAsync();
            await base.OnActivateAsync();
        }
    }
}