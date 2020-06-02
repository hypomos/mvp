namespace Hypomos.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WhoAmIController : ControllerBase
    {
        [HttpGet]
        public WhoAmI Get()
        {
            var isLoggedIn = this.User.Identity.IsAuthenticated;
            
            return new WhoAmI
            {
                IsLoggedIn = isLoggedIn
            };
        }
    }
}