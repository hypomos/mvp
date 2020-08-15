// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Hypomos.IdentityServer
{
    using System.Collections.Generic;
    using System.Linq;
    using IdentityServer4.Models;

    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
                {new ApiScope("hypomos", "Hypomos API")};

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "swagger-api-id",
                    ClientSecrets =
                    {
                        new Secret("some-even-more-secret-secret".Sha256())
                    },
                    AllowedScopes = AllScopes(),
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:5010/swagger/oauth2-redirect.html"
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientName = "hypomos-web-app",
                    ClientId = "hypomos-web-app",
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 330,
                    IdentityTokenLifetime = 300,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
 
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:3000/",
                        "https://localhost:3000"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:3000"
                    },

                    AllowedScopes = AllScopes(),
                    RedirectUris = new List<string>
                    {
                        "https://localhost:3000/app",
                        "https://localhost:3000/callback",
                        "https://localhost:3000/silent-renew",
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                }
            };

        private static List<string> AllScopes()
        {
            return ApiScopes
                .Select(r => r.Name)
                .Concat(new List<string> {"openid",
                    "role",
                    "profile",
                    "email"})
                .ToList();
        }
    }
}