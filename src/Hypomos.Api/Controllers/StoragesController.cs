namespace Hypomos.Api.Controllers
{
    using System.Security.Claims;
    using Hypomos.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Orleans;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoragesController : ControllerBase
    {
        private readonly IClusterClient client;

        public StoragesController(IClusterClient client)
        {
            this.client = client;
        }

        [HttpGet]
        public void Get()
        {
            var name = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var bwalti = this.client.GetGrain<IUserGrain>(name?.Value);
        }
    }
}