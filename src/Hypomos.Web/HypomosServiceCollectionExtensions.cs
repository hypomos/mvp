namespace Hypomos.Web
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Hypomos.Interfaces;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using Orleans;

    public static class HypomosServiceCollectionExtensions
    {
        public static void AddHypomosServices(this IServiceCollection services)
        {
            services.AddScoped(HypomosUserFactory);
        }

        private static HypomosUser HypomosUserFactory(IServiceProvider arg)
        {
            var stateProvider = arg.GetRequiredService<AuthenticationStateProvider>();
            var authenticationState = stateProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult();

            var clusterClient = arg.GetRequiredService<IClusterClient>();

            var uniqueUsername = authenticationState.User.FindFirst(claim => claim.Type == ClaimTypes.Name);
            var userGrain = clusterClient.GetGrain<IUserGrain>(uniqueUsername.Value);

            var state = userGrain.GetState().GetAwaiter().GetResult();
            if (state.IsInitialized)
            {
                return new HypomosUser
                {
                    Username = state.Username,
                    Email = state.EmailAddress,
                    Surname = state.Surname,
                    GivenName = state.GivenName
                };
            }

            var dict = authenticationState.User.Claims.ToLookup(c => c.Type, c => c.Value);
            return new HypomosUser
            {
                Username = dict[ClaimTypes.Name].FirstOrDefault() ?? string.Empty,
                Email = dict[ClaimTypes.Email].FirstOrDefault() ?? string.Empty,
                Surname = dict[ClaimTypes.Surname].FirstOrDefault() ?? string.Empty,
                GivenName = dict[ClaimTypes.GivenName].FirstOrDefault() ?? string.Empty
            };
        }
    }
}