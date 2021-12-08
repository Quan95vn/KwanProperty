using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwanProperty.User.Api.Authorization
{
    public class CustomRequirementHandler : AuthorizationHandler<CustomRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            CustomRequirement requirement)
        {
            var id = _httpContextAccessor.HttpContext.GetRouteValue("id").ToString();

            // call service to check
            if (id != "ABC")
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // all checks out
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
