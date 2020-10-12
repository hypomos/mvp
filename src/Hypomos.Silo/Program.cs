namespace Hypomos.Silo
{
    using System;
    using System.Threading.Tasks;

    using Hypomos.GrainInterfaces;
    using Hypomos.Grains;
    using Hypomos.Silo.Hosting;
    using Hypomos.Silo.Storage;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Runtime;
    using Orleans.Storage;
#if DEBUG
    using System.Net;
#endif
#if !DEBUG
    using Orleans.Clustering.Kubernetes;
#endif

    public class Program
    {
        public static Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile("appsettings.Development.json", true)
#endif
                .AddEnvironmentVariables("HYPOMOS_")
                .Build();

            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    var random = new Random();

                    builder
                        .AddMemoryGrainStorageAsDefault()
                        .AddSimpleMessageStreamProvider(Constants.SmsProvider)
                        .AddMemoryGrainStorage("PubSubStore")
                        .AddMinioGrainStorage("minio-orleans", options =>
                        {
                            config.GetSection("OrleansMinioStorage").Bind(options);
                        })
#if DEBUG
                        .UseLocalhostClustering()
#else
                        .ConfigureEndpoints(random.Next(10001, 10100), random.Next(20001, 20100))
                        .UseKubeMembership()
#endif
                        .Configure<ClusterOptions>(options => config.GetSection("Orleans").Bind(options))
#if DEBUG
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
#endif
                        .ConfigureApplicationParts(parts =>
                            parts.AddApplicationPart(typeof(UserGrain).Assembly).WithReferences())
                        .UseDashboard(options =>
                        {
                            options.HostSelf = true;
                            options.Port = 9090;
                            options.BasePath = "/";
                        });
                })
                .ConfigureServices(services =>
                {
                    //var minioOrleans = "minio-orleans";

                    //services.AddOptions<MinioGrainStorageOptions>(minioOrleans);
                    //services.AddSingletonNamedService(minioOrleans, MinioGrainStorageFactory.Create)
                    //    .AddSingletonNamedService(minioOrleans,
                    //        (s, n) =>
                    //            (ILifecycleParticipant<ISiloLifecycle>) s.GetRequiredServiceByName<IGrainStorage>(n));

                    //services.Configure<MinioGrainStorageOptions>(minioOrleans, config =>
                    //{
                    //    config.AccessKey = "minio";
                    //    config.SecretKey = "minio123";
                    //    config.Endpoint = "localhost:9000";
                    //    config.Container = "grain-storage";
                    //});

                    services.Configure<ConsoleLifetimeOptions>(options => { options.SuppressStatusMessages = true; });
                })
                .ConfigureLogging(builder => { builder.AddConsole(); })
                .RunConsoleAsync();
        }
    }
}