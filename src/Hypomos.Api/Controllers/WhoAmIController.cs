namespace Hypomos.Api.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class WhoAmIController : ControllerBase
    {
        [HttpGet]
        public WhoAmI Get()
        {
            var isLoggedIn = this.User.Identity.IsAuthenticated;

            if (isLoggedIn)
            {
            }

            return new WhoAmI
            {
                IsLoggedIn = isLoggedIn
            };
        }
    }

    public class WhoAmI
    {
        public bool IsLoggedIn { get; set; }
        public WhoAmIDetails UserDetails { get; set; }
    }

    public class WhoAmIDetails
    {
        public string Email { get; set; }
        public string LastName { get; set; }
        public string GivenName { get; set; }
    }
}