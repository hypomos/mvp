namespace Hypomos.IdentityServer.Middleware
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class HttpInspectorMiddleware
    {
        private readonly ILogger logger;

        private readonly RequestDelegate next;

        public HttpInspectorMiddleware(RequestDelegate next, ILogger<HttpInspectorMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            this.LogRequest(context);
            await this.next(context);
        }

        private void LogRequest(HttpContext context)
        {
            var buffer = new StringBuilder();

            context.Request.Headers.ToList()
                .ForEach(kvp => buffer.Append($"{kvp.Key}: {kvp.Value}{Environment.NewLine}"));

            this.logger.LogDebug($"Http Request Information:{Environment.NewLine}" + $"Schema: {context.Request.Scheme}{Environment.NewLine}" + $"Host: {context.Request.Host}{Environment.NewLine}" + $"Path: {context.Request.Path}{Environment.NewLine}" + $"QueryString: {context.Request.QueryString}{Environment.NewLine}" + $"Headers: {Environment.NewLine}{buffer}");
        }
    }
}