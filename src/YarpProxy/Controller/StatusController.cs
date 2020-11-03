namespace YarpProxy.Controller
{
    using System.Runtime.InteropServices;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    [Route("/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ILogger<StatusController> logger;

        public StatusController(IWebHostEnvironment hostEnvironment, IConfiguration configuration, ILogger<StatusController> logger)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public ActionResult GetStatus()
        {
            return this.Ok(new { Environment = this.hostEnvironment.EnvironmentName, Platform = RuntimeInformation.FrameworkDescription });
        }
    }
}