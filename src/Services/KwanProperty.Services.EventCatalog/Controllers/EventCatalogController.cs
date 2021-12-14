using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwanProperty.Services.EventCatalog.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventCatalogController : ControllerBase
    {
        public EventCatalogController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            var result = await Task.FromResult("From EventCatalog Service");
            return Ok(result);
        }
    }
}