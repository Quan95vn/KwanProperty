using KwanProperty.IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KwanProperty.IdentityServer4.Quickstart.UserRegistration.PasswordReset
{
    public class PasswordResetController : Controller
    {
        private readonly IUserService _userService;

        public PasswordResetController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public IActionResult RequestPassword()
        {
            var vm = new RequestPasswordViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestPassword(RequestPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var securityCode = await _userService.InitiatePasswordResetRequest(model.Email);
            await _userService.SaveChangesAsync();

            // create an activation link
            var link = Url.ActionLink("ResetPassword", "PasswordReset", new { securityCode });
            Debug.WriteLine(link);

            return View("PasswordResetRequestSent");
        }

        [HttpGet]
        public IActionResult ResetPassword(string securityCode)
        {
            var vm = new ResetPasswordViewModel()
            {
                SecurityCode = securityCode
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _userService.SetPassword(model.SecurityCode, model.Password))
            {
                ViewData["Message"] = "Your password was successfully changed.  " +
                    "Navigate to your client application to log in.";
            }
            else
            {
                ViewData["Message"] = "Your password couldn't be changed, please" +
                    " contact your administrator.";
            }

            await _userService.SaveChangesAsync();

            return View("ResetPasswordResult");
        }
    }
}
