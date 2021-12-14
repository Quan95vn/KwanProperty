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
        /// An identity resource is a named group of claims that can be requested using the scope parameter
        /// Once the resource is defined, you can give access to it to a client via the AllowedScopes option
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
        /// Using the API resource support for the JWT aud claim. The value(s) of the audience claim will be the name of the API resource(s)
        /// Khai báo ApiResource, tên của ApiResource được đùng để check với audience của các service API (option.Audience) để check có quyền truy cập tới service API ko 
        /// Scopes được dùng trong AllowedScope của Client
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
             new ApiResource[]
             {
                new ApiResource("KwanPropertyUserApi", "Kwan Property User Api")
                {
                    Scopes = { "KwanPropertyUserApi" }, // Tên scope lấy từ ApiScope
                    ApiSecrets = { new Secret("KwanPropertyUserApiSecret".Sha256())}
                },
                new ApiResource("KwanPropertyEventCatalog", "Event catalog API")
                {
                    Scopes = { "KwanPropertyEventCatalog.FullAccess" }
                },
                new ApiResource("KwanPropertyGateway", "Event catalog API")
                {
                    Scopes = { "KwanPropertyGateway.FullAccess" }
                },
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
                    new List<string> { "admin", "super_user", "moderator" } // claim sẽ được thêm trong access_token
                ), 
                new ApiScope("KwanPropertyEventCatalog.FullAccess"),
                new ApiScope("KwanPropertyGateway.FullAccess"),
            };

        /// <summary>
        /// Clients represent applications that can request tokens from your identityserver.
        /// Client đại diện cho ứng dụng có thể yêu cầu token từ  IdentityServer
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
                    // AllowedScope có thể vừa là tên của IdentityResource, vừa là Scope của ApiResource, phía client truy cập tới AllowedScope -> options.Scope.Add(...)
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

                        //Tên của ApiScope dùng để xác định scope này thuộc ApiResource nào -> tên của ApiResource là tên của Audience
                        "KwanPropertyGateway.FullAccess", "KwanPropertyEventCatalog.FullAccess",

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