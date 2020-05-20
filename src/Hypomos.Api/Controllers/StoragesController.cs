namespace Hypomos.Api.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;
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
        public async Task<IEnumerable<StorageConfiguration>> Get()
        {
            var name = this.User.FindFirst(ClaimTypes.NameIdentifier);
            var user = this.client.GetGrain<IUserGrain>(name?.Value);

            return await user.GetStorageProviders();
        }
    }
}