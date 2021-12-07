using IdentityModel.Client;
using KwanProperty.MvcClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KwanProperty.MvcClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                await WriteOutIdentityInformation();

                var httpClient = _httpClientFactory.CreateClient("KwanUserApiClient");

                var request = new HttpRequestMessage(
                 HttpMethod.Get,
                 "/api/account/");

                var response = await httpClient.SendAsync(
                    request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        //return View(new GalleryIndexViewModel(
                        //    await JsonSerializer.DeserializeAsync<List<Image>>(responseStream)));
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                        response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("AccessDenied", "Authorization");
                }
            }
            catch(Exception ex)
            {
            }

            return View();
        }

        private async Task WriteOutIdentityInformation()
        {
            var identityToken = await HttpContext
            .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // write it out
            Debug.WriteLine($"Identity token: {identityToken}");

            // write out the user claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }


        public async Task<IActionResult> Address()
        {
            var idpClient = _httpClientFactory.CreateClient("IdentityServerClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new Exception(
                    "Problem accessing the discovery endpoint."
                    , metaDataResponse.Exception);
            }

            var accessToken = await HttpContext
              .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
               new UserInfoRequest
               {
                   Address = metaDataResponse.UserInfoEndpoint,
                   Token = accessToken
               });

            if (userInfoResponse.IsError)
            {
                throw new Exception(
                    "Problem accessing the UserInfo endpoint."
                    , userInfoResponse.Exception);
            }

            var address = userInfoResponse.Claims
                .FirstOrDefault(c => c.Type == "address")?.Value;

            return View(new HomeViewModel(address));
        }


        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
