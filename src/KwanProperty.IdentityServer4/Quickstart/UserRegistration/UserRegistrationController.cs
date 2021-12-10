using IdentityModel;
using KwanProperty.IdentityServer4.Entities;
using KwanProperty.IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KwanProperty.IdentityServer4.Quickstart.UserRegistration
{
    public class UserRegistrationController : Controller
    {
        private readonly IUserService _userService;

        public UserRegistrationController(
            IUserService userService
            )
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> ActivateUser(string securityCode)
        {
            if (await _userService.ActivateUser(securityCode))
            {
                ViewData["Message"] = "Your account was successfully activated.  " +
                    "Navigate to your client application to log in.";
            }
            else
            {
                ViewData["Message"] = "Your account couldn't be activated, " +
                    "please contact your administrator.";
            }

            await _userService.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser(string returnUrl)
        {
            var vm = new RegisterUserViewModel{ ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userToCreate = new User
            {
                Username = model.UserName,
                Subject = Guid.NewGuid().ToString(),
                Email = model.Email,
                Active = false
            };

            userToCreate.Claims.Add(new UserClaim()
            {
                Type = "country",
                Value = model.Country
            });

            userToCreate.Claims.Add(new UserClaim()
            {
                Type = JwtClaimTypes.Address,
                Value = model.Address
            });

            userToCreate.Claims.Add(new UserClaim()
            {
                Type = JwtClaimTypes.GivenName,
                Value = model.GivenName
            });

            userToCreate.Claims.Add(new UserClaim()
            {
                Type = JwtClaimTypes.FamilyName,
                Value = model.FamilyName
            });

            _userService.AddUser(userToCreate, model.Password);
            await _userService.SaveChangesAsync();

            // create an activation link
            var link = Url.ActionLink("ActivateUser", "UserRegistration",
                new { securityCode = userToCreate.SecurityCode });
            // TODO: implement send mail
            Debug.WriteLine(link);

            return View("ActivationCodeSent");

            //// log the user in
            //await HttpContext.SignInAsync(userToCreate.Subject, userToCreate.Username);

            //// continue with the flow     
            //if (_interaction.IsValidReturnUrl(model.ReturnUrl)
            //    || Url.IsLocalUrl(model.ReturnUrl))
            //{
            //    return Redirect(model.ReturnUrl);
            //}

            //return Redirect("~/");

        }
    }
}
