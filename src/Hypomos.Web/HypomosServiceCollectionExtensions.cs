namespace Hypomos.Web
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using Orleans;

    public static class HypomosServiceCollectionExtensions
    {
        public static void AddHypomosServices(this IServiceCollection services)
        {
            services.AddScoped<HypomosUserFactory>();
        }
    }

    public class HypomosUserFactory
    {
        private readonly AuthenticationStateProvider stateProvider;
        private readonly IClusterClient clusterClient;

        public HypomosUserFactory(AuthenticationStateProvider stateProvider, IClusterClient clusterClient)
        {
            this.stateProvider = stateProvider;
            this.clusterClient = clusterClient;
        }
        public async Task<HypomosUser> GetCurrentUserAsync()
        {
            var authenticationState = await stateProvider.GetAuthenticationStateAsync();

            var uniqueUsername = authenticationState.User.FindFirst(claim => claim.Type == ClaimTypes.Name);
            if (uniqueUsername == null)
            {
                return new HypomosUser();
            }

            var userGrain = this.clusterClient.GetGrain<IUserGrain>(uniqueUsername.Value);

            //var state = await userGrain.GetSetupStateAsync();
            //if (state.ArePersonalDetailsSet)
            //{
            //    var userData = await userGrain.GetPersonalDetails();
            //    return new HypomosUser(userData.Username, userData.EmailAddress, userData.GivenName, userData.Surname);
            //}

            var dict = authenticationState.User.Claims.ToLookup(c => c.Type, c => c.Value);
            
            var username = dict[ClaimTypes.Name].FirstOrDefault() ?? string.Empty;
            var email = dict[ClaimTypes.Email].FirstOrDefault() ?? string.Empty;
            var surname = dict[ClaimTypes.Surname].FirstOrDefault() ?? string.Empty;
            var givenName = dict[ClaimTypes.GivenName].FirstOrDefault() ?? string.Empty;
            
            return new HypomosUser(username, email, givenName, surname);
        }
    }
}