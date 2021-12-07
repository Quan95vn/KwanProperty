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

                 Claims = new List<Claim>
                 {
                     new Claim("given_name", "Quan"),
                     new Claim("family_name", "Tran"),
                     new Claim("address", "Kham Thien 1"),
                     new Claim("role1", "FreeUser1")
                 }
             },
             new TestUser
             {
                 SubjectId = "00000000-0000-0000-0000-000000000002",
                 Username = "Mai",
                 Password = "12345678",

                 Claims = new List<Claim>
                 {
                     new Claim("given_name", "Mai"),
                     new Claim("family_name", "Tran"),
                     new Claim("address", "Kham Thien 2"),
                     new Claim("role1", "PayingUser")
                 }
             }
         };
    }
}