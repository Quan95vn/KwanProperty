﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace KwanProperty.IdentityServer4
{
    public static class Config
    {
        /// <summary>
        /// Danh sách tài nguyên được phép truy cập, trong mỗi tài nguyên có thể truy cập tới nhiều claim
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
        /// Khai báo tài nguyên api
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
                    Scopes = { "KwanPropertyUserApi" }, // Tên scope phải match với ApiScopes
                    ApiSecrets = { new Secret("KwanPropertyUserApiSecret".Sha256())}
                }
             };

        /// <summary>
        /// Phạm vi scope của api, truy cập tới scope của claim qua access_token
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(
                    "KwanPropertyUserApi",
                    "KwanProperty Api Scope",
                    new List<string> { "admin", "super_user"}) // claim sẽ được thêm trong access_token
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
                        "KwanPropertyUserApi", // tên api scope
                        "subscription_level",
                        "country"
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