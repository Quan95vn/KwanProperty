using Microsoft.AspNetCore.Authorization;

namespace KwanProperty.User.Api.Authorization
{
    public class CustomRequirement : IAuthorizationRequirement
    {
        public CustomRequirement()
        {
        }
    }
}
