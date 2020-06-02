namespace Hypomos.ReactSPA.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpGet]
        public HypomosConfiguration Get()
        {
            return new HypomosConfiguration(new ApiEndpoints(), new OneDriveConfiguration());
        }
    }

    public class HypomosConfiguration
    {
        public HypomosConfiguration(ApiEndpoints apiEndpoints, OneDriveConfiguration oneDrive)
        {
            this.MachineName = System.Environment.MachineName;
            this.ApiEndpoints = apiEndpoints;
            this.OneDrive = oneDrive;
        }

        public string MachineName { get; set; }

        public ApiEndpoints ApiEndpoints { get; set; }

        public OneDriveConfiguration OneDrive { get; set; }
    }

    public class OneDriveConfiguration
    {
        public string AppId { get; set; } = "9526c3d5-5fb0-47a9-b069-bf58254f0f45";
        public string RedirectUri { get; set; } = "http://localhost:3000/app/storages/auth-onedrive";
        public List<string> Scopes { get; set; } = new List<string>{ "User.Read", "offline_access", "Files.ReadWrite.All" };
    }

    public class ApiEndpoints
    {
        public string Collection { get; set; } = "https://jsonplaceholder.typicode.com/";
        public string MediaItems { get; set; } = "https://jsonplaceholder.typicode.com/";
        public string Hypomos { get; set; } = "http://localhost:5010/api/";
        public string WhoAmI { get; set; } = "http://localhost:5010/api/";
    }
}