namespace Hypomos.ReactSPA.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpGet]
        public HypomosConfiguration Get()
        {
            return new HypomosConfiguration(new ApiEndpoints());
        }
    }

    public class HypomosConfiguration
    {
        public HypomosConfiguration(ApiEndpoints apiEndpoints)
        {
            this.MachineName = System.Environment.MachineName;
            this.ApiEndpoints = apiEndpoints;
        }

        public string MachineName { get; set; }

        public ApiEndpoints ApiEndpoints { get; set; }
    }

    public class ApiEndpoints
    {
        public string Collection { get; set; } = "https://jsonplaceholder.typicode.com/";
        public string MediaItems { get; set; } = "https://jsonplaceholder.typicode.com/";
        public string Hypomos { get; set; } = "http://localhost:5010/api/";
        public string WhoAmI { get; set; } = "http://localhost:5010/api/";
    }
}