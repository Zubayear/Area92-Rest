// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Area92.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope()
                {
                    Name = "API",
                    DisplayName = "My API",
                }
            };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>()
        {
            new ApiResource("API", "MY API")
            {
                Scopes = { "API" }
            }
        };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId = "DEMON",
                    ClientName = "DEMON",
                    AllowedScopes = new List<string>()
                    {
                        "API",
                    },
                    ClientSecrets =
                    {
                        new Secret()
                        {
                            Value = "DEMON".Sha256()
                        }
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials
                }
            };
    }
}