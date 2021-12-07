// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace KwanProperty.IdentityServer4
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource(
                    "roles",
                    "Your's role(s)",
                    new List<string>
                    {
                        "role1", "role2", "role3", "role4"
                    })
                    
            };

        public static IEnumerable<ApiResource> Apis =>
             new ApiResource[]
             {
                new ApiResource(
                    "KwanPropertyUserApi",
                    "Kwan Property User Api",
                    new List<string>() { "role" })
             };

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("KwanPropertyUserApi")
            };
        }

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Mvc",
                    ClientId= "Mvc_Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44327/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44327/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "KwanPropertyUserApi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                },
                new Client
                {
                    ClientName = "Mobile",
                    ClientId = "Mobile_Client",
                    AllowedGrantTypes = GrantTypes.Code,
                }
            };
    }
}