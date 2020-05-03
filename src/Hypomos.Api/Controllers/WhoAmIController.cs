namespace Hypomos.Api.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WhoAmIController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in this.User.Claims select new {c.Type, c.Value});
        }
    }
}