using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwanProperty.User.Api.Authorization
{
    public class CustomRequirement : IAuthorizationRequirement
    {
        public CustomRequirement()
        {
        }
    }
}
