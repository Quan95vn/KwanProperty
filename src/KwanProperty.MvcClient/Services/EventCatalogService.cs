using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KwanProperty.MvcClient.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient _httpClient;

        public EventCatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAll()
        {
            var response = await _httpClient.GetAsync("api/events");
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
