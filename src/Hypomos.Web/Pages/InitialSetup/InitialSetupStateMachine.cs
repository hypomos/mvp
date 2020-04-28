namespace Hypomos.Web.Pages.InitialSetup
{
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;
    using Orleans;
    using Stateless;

    public class InitialSetupStateMachine
    {
        private readonly HypomosUser user;
        private readonly IClusterClient client;

        private StateMachine<InitialSetupStates, InitialSetupTriggers>? stateMachine;
        private IUserGrain userGrain;

        public InitialSetupStateMachine(HypomosUser user, IClusterClient client)
        {
            this.user = user;
            this.client = client;

            this.Config = new StateMachine<InitialSetupStates, InitialSetupTriggers>(InitialSetupStates.BasicUserInfo);

            this.Config.Configure(InitialSetupStates.BasicUserInfo)
                .PermitReentry(InitialSetupTriggers.Refresh)
                .Permit(InitialSetupTriggers.Next, InitialSetupStates.StorageSources)
                .OnEntry(() => this.NavigationTarget = $"InitialSetup/{nameof(PersonalDetails)}");

            this.Config.Configure(InitialSetupStates.StorageSources)
                .PermitReentry(InitialSetupTriggers.Refresh)
                .Permit(InitialSetupTriggers.Previous, InitialSetupStates.BasicUserInfo)
                .Permit(InitialSetupTriggers.Next, InitialSetupStates.Finished)
                .OnEntry(() => this.NavigationTarget = $"InitialSetup/{nameof(StorageSources)}");

            this.Config.Configure(InitialSetupStates.Finished)
                .PermitReentry(InitialSetupTriggers.Refresh)
                .OnEntry(() => this.NavigationTarget = "Dashboard");
        }

        public StateMachine<InitialSetupStates, InitialSetupTriggers> Config { get; set; }

        public bool ShouldNavigate => !string.IsNullOrEmpty(this.NavigationTarget);
        
        public string? NavigationTarget { get; private set; }

        public async Task Init()
        {
            await this.CreateStateMachine();
            await this.stateMachine.FireAsync(InitialSetupTriggers.Refresh);
        }

        private async Task CreateStateMachine()
        {
            this.userGrain = this.client.GetGrain<IUserGrain>(this.user.Username);
            //var setupState = await this.userGrain.GetSetupStateAsync();

            //if (!setupState.ArePersonalDetailsSet)
            //{
            //    this.stateMachine = StateMachineFactory.Create(InitialSetupStates.BasicUserInfo, this.Config);
            //}
            //else if (!setupState.AreStorageSourcesSet)
            //{
            //    this.stateMachine = StateMachineFactory.Create(InitialSetupStates.StorageSources, this.Config);
            //}
            //else
            //{
            //    this.stateMachine = StateMachineFactory.Create(InitialSetupStates.Finished, this.Config);
            //}

        }

        private async Task Next()
        {
            await this.stateMachine.FireAsync(InitialSetupTriggers.Next);
        }

        public async Task Previous()
        {
            await this.stateMachine.FireAsync(InitialSetupTriggers.Previous);
        }

        public async Task SetPersonalDetails(UserPersonalDetails userPersonalDetails)
        {
            await this.userGrain.SetPersonalDetails(userPersonalDetails);
            await this.Next();
        }
    }
}