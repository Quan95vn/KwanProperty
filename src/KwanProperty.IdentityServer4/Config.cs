// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace KwanProperty.IdentityServer4
{
    public static class Config
    {
        /// <summary>
        /// Danh sách tài nguyên được phép truy cập, trong mỗi tài nguyên có chứa nhiều claim khác nhau
        /// </summary>
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
                        "admin", "super_user", "moderator", "user"
                    }),
                 new IdentityResource(
                    "subscription_level",
                    "Subscription Level",
                    new List<string>
                    {
                        "subscription_level"
                    }),
                 new IdentityResource(
                    "country",
                    "Country",
                    new List<string>
                    {
                        "country"
                    }),

            };

        /// <summary>
        /// Phạm vi scope của api, api được truy cập tới claim nào
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(
                    "KwanPropertyUserApi", 
                    "KwanProperty Api Scope",
                    new List<string> { "admin", "super_user"}) // claim sẽ được thêm trong access_token
            };
        }

        public static IEnumerable<ApiResource> ApiResources =>
             new ApiResource[]
             {
                new ApiResource
                (
                    "KwanPropertyUserApi",
                    "Kwan Property User Api"
                )
                {
                    Scopes = { "KwanPropertyUserApi"},
                    ApiSecrets = { new Secret("KwanPropertyUserApiSecret".Sha256())}
                }
             };

        /// <summary>
        /// Danh sách client và config tài nguyền client đc truy cập
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    AccessTokenLifetime = 120,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
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
                        "KwanPropertyUserApi",
                        "subscription_level",
                        "country"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
    }
}