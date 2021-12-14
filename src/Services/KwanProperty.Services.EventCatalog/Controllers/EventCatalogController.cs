using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace KwanProperty.Services.EventCatalog.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Authorize]
    public class EventCatalogController : ControllerBase
    {
        public EventCatalogController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            // sau khi pass token từ gateway uống api, ta có thể truy cập tới Claim  qua User.Claims
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            var result = await Task.FromResult("From EventCatalog Service");
            return Ok(result);
        }
    }
}