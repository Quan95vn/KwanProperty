using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwanProperty.IdentityServer4.Services
{
    public class UserProfileService : IProfileService
    {
        private readonly IUserService _userService;

        public UserProfileService(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var claimsForUser = (await _userService.GetUserClaimsBySubjectAsync(subjectId))
                .ToList();

            context.AddRequestedClaims(
                claimsForUser.Select(c => new Claim(c.Type, c.Value)).ToList());
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            context.IsActive = await _userService.IsUserActive(subjectId);
        }
    }
}
