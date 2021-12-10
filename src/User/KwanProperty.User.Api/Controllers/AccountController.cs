using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwanProperty.User.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return Ok("success");
        }

        [HttpGet("custom-claim")]
        public IActionResult GetCustomClaimOpenIdConnectPostConfiguration()
        {
            return Ok("PaidUser");
        }
    }
}
