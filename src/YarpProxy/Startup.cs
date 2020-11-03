namespace YarpProxy
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.ReverseProxy.Middleware;
    using YarpProxy.Middleware;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddReverseProxy()
                .LoadFromConfig(this.Configuration.GetSection("ReverseProxy"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseMiddleware<HttpInspectorMiddleware>();

            app.UseEndpoints(
                endpoints =>
                {
                    //endpoints.MapControllers();
                    endpoints.MapReverseProxy(
                        proxyPipeline =>
                        {
                            proxyPipeline.Use(
                                (context, next) =>
                                {
                                    var lf = proxyPipeline.ApplicationServices.GetRequiredService<ILoggerFactory>();
                                    var logger = lf.CreateLogger("Proxy.Middleware");

                                    var destinationsFeature = context.Features.Get<IReverseProxyFeature>();

                                    logger.LogDebug("Iterating Destinations");
                                    foreach (var dest in destinationsFeature.AvailableDestinations)
                                    {
                                        logger.LogDebug($"Destination ID: {dest.DestinationId}\nAddress: {dest.Config.Address}\nHealth: {dest.DynamicState?.Health}");
                                    }

                                    return next();
                                });
                        });
                });
        }
    }
}