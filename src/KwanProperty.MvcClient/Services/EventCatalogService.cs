using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace KwanProperty.MvcClient.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventCatalogService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetAll()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            _httpClient.SetBearerToken(accessToken);

            var response = await _httpClient.GetAsync("api/events");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                return "Unauthorized";
            }
        }
    }
}
