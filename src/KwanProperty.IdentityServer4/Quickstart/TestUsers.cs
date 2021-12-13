// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "00000000-0000-0000-0000-000000000001",
                Username = "Quan",
                Password = "12345678",

                // Claim lấy trong claims nếu IdentityResources có trong AllowedScope của client, sẽ dc gen ra trong identity token
                Claims = new List<Claim>
                {
                    new Claim("given_name", "Quan"),
                    new Claim("family_name", "Tran"),
                    new Claim("address", "Kham Thien 1"),
                    
                    new Claim("subscription_level", "PaidUser"),
                    new Claim("subscription_level1", "PaidUser1"),

                    new Claim("country", "vn"),
                    new Claim("country1", "vn1"),

                    new Claim("admin", "admin"),
                    new Claim("super_user", "super_user"),
                    new Claim("moderator", "moderator"),

                    new Claim("IdentityNumber_Old", "IdentityNumber_Old"),
                    new Claim("IdentityNumber_New", "IdentityNumber_New")
                }
            },
            //new TestUser
            //{
            //    SubjectId = "00000000-0000-0000-0000-000000000002",
            //    Username = "Mai",
            //    Password = "12345678",

            //    Claims = new List<Claim>
            //    {
            //        new Claim("given_name", "Mai"),
            //        new Claim("family_name", "Tran"),
            //        new Claim("address", "Kham Thien 2"),
            //        new Claim("user", "user"),

            //        new Claim("subscription_level", "FreeUser"),
            //        new Claim("country", "vn"),
            //    }
            //}
         };
    }
}