using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;

namespace KwanProperty.MvcClient.PostConfigurationOptions
{
    public class OpenIdConnectOptionsPostConfigureOptions
        : IPostConfigureOptions<OpenIdConnectOptions>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenIdConnectOptionsPostConfigureOptions(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Lấy custom claim từ phía API và thêm vào claim hiện tại
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void PostConfigure(string name, OpenIdConnectOptions options)
        {
            options.Events = new OpenIdConnectEvents()
            {
                OnTicketReceived = async ticketReceivedContext =>
                {
                    var apiClient = _httpClientFactory.CreateClient("BasicUserApiClient");

                    var request = new HttpRequestMessage(
                        HttpMethod.Get,
                        $"/api/account/custom-claim");
                    request.SetBearerToken(
                        ticketReceivedContext.Properties.GetTokenValue("access_token"));

                    var response = await apiClient.SendAsync(
                        request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();

                    var subscriptionlevel = string.Empty;
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        subscriptionlevel = await JsonSerializer.DeserializeAsync<string>(responseStream);
                    }

                    var newClaimsIdentity = new ClaimsIdentity();
                    newClaimsIdentity.AddClaim(
                        new Claim("subscription_level", subscriptionlevel));

                    // add this additional identity
                    ticketReceivedContext.Principal.AddIdentity(newClaimsIdentity);
                }
            };
        }
    }
}
