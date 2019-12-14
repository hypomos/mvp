namespace Hypomos.Silo
{
    using System.Net;
    using System.Threading.Tasks;
    using Hypomos.Grains;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Persistence.Minio;
    using Orleans.Persistence.Minio.Storage;
    using Orleans.Providers;
    using Orleans.Runtime;
    using Orleans.Storage;

    public class Program
    {
        public static Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder
                        .Configure<ClusterOptions>(options => config.GetSection("Orleans").Bind(options))
                        //.AddMemoryGrainStorageAsDefault()
#if DEBUG
                        .UseLocalhostClustering()
#else
                        .UseKubeGatewayListProvider()
#endif
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts =>
                            parts.AddApplicationPart(typeof(UserGrain).Assembly).WithReferences())
                        .UseDashboard(options =>
                        {
                            options.HostSelf = true;
                            options.Port = 9000;
                            options.BasePath = "/";
                        });
                })
                .ConfigureServices(services =>
                {
                    var minioOrleans = "minio-orleans";

                    services.AddOptions<MinioGrainStorageOptions>(minioOrleans);
                    services.AddSingletonNamedService(minioOrleans, MinioGrainStorageFactory.Create)
                        .AddSingletonNamedService(minioOrleans, (s,n) => (ILifecycleParticipant<ISiloLifecycle>)s.GetRequiredServiceByName<IGrainStorage>(n));

                    services.Configure<MinioGrainStorageOptions>(minioOrleans, config =>
                    {
                        config.AccessKey = "minio";
                        config.SecretKey = "minio123";
                        config.Endpoint = "192.168.5.15:9000";
                        config.Container = "grain-storage";
                    });

                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
