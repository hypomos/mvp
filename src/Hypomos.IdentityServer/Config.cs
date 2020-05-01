// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Hypomos.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("hypomos", "Hypomos API")
                {
                    Scopes = { new Scope("hypomos.read") }
                },
                new ApiResource("Files", "MS Files")
                {
                    Scopes = { new Scope("Files.ReadWrite.All") }
                }, 
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "swagger-api-id",
                    ClientSecrets =
                    {
                        new Secret("some-even-more-secret-secret".Sha256())
                    },
                    AllowedScopes = {"hypomos.read", "Files.ReadWrite.All"},
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:5010/swagger/oauth2-redirect.html"
                    },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                },
            };
    }
}