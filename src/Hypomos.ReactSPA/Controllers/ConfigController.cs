namespace Hypomos.ReactSPA.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpGet]
        public AppConfiguration Get()
        {
            return new AppConfiguration
            {
                SomeSetting = "Hello World!"
            };
        }
    }

    public class AppConfiguration
    {
        public string SomeSetting { get; set; }
    }
}