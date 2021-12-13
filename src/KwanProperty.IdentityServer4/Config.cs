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
        /// Danh sách tài nguyên được phép truy cập, User có thể truy cập tới claim nếu IdentityResource nằm trong AllowedScope của client
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
                        "subscription_level", "subscription_level1", "subscription_level2"
                    }),
                 new IdentityResource(
                    "country",
                    "Country",
                    new List<string>
                    {
                        "country", "country1", "country2", "country3"
                    }),

                new IdentityResource(
                    "IdentityNumber",
                    "IdentityNumber",
                    new List<string>
                    {
                        "IdentityNumber_Old", "IdentityNumber_New"
                    }),

            };

        /// <summary>
        /// Khai báo ApiResource, tên của ApiResource được đùng để check với audience của các service API (option.Audience)
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
             new ApiResource[]
             {
                new ApiResource
                (
                    "KwanPropertyUserApi",
                    "Kwan Property User Api"
                )
                {
                    Scopes = { "KwanPropertyUserApi" }, // Tên scope lấy từ ApiScope
                    ApiSecrets = { new Secret("KwanPropertyUserApiSecret".Sha256())}
                }
             };

        /// <summary>
        /// Khai báo ApiScope, scope dành cho ApiResource
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(
                    "KwanPropertyUserApi",
                    "KwanProperty Api Scope",
                    new List<string> { "admin", "super_user", "moderator" }) // claim sẽ được thêm trong access_token
            };

        /// <summary>
        /// Danh sách client và config tài nguyền client đc truy cập
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    AccessTokenLifetime = 30,
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

                    AlwaysIncludeUserClaimsInIdToken = false,
                    // AllowedScope có thể vừa là tên của IdentityReource, vừa là tên của ApiResource
                    AllowedScopes =
                    {
                        // tên của IdentityResource
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "subscription_level",
                        "country",
                        "IdentityNumber",
                        // tên của ApiResource
                        "KwanPropertyUserApi", 
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };

        #region SQL Tables
        /*
            SELECT * FROM "ApiResources"
            SELECT * FROM "ApiResourceScopes"
            SELECT * FROM "ApiResourceClaims"

            SELECT * FROM "ApiScopes"
            SELECT * FROM "ApiScopeClaims"
            SELECT * FROM "ClientGrantTypes"

            SELECT * FROM "ClientRedirectUris"
            SELECT * FROM "ClientScopes"
            SELECT * FROM "ClientSecrets"
            SELECT * FROM "Clients"
            SELECT * FROM "IdentityResources";
            SELECT * FROM "IdentityResourceClaims";

            SELECT * FROM "DeviceCodes"
            SELECT * FROM "PersistedGrants"
         */

        #endregion
    }
}