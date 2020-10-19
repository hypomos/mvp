namespace Hypomos.BlazorApp
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Steeltoe.Common;
    using Steeltoe.Extensions.Configuration.Kubernetes;
    using Steeltoe.Extensions.Logging;
    using Steeltoe.Management.Kubernetes;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(
                builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Debug)
                        .AddConsole();
                });

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                .ConfigureAppConfiguration(
                    (context, builder) =>
                    {
                        builder.AddEnvironmentVariables("HYPOMOS_");
                        builder.AddKubernetes(loggerFactory: GetLoggerFactory());

                        Console.WriteLine(Platform.IsKubernetes ? "!!! WE ARE ON K8S !!!" : "!!! WE ARE ---NOT--- ON K8S !!!");
                    })
                .ConfigureLogging(
                    (builderContext, loggingBuilder) =>
                    {
                        loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                        loggingBuilder.AddDynamicConsole();
                    })
                .AddKubernetesActuators()
                ;
        }

        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace));
            serviceCollection.AddLogging(
                builder => builder.AddConsole(
                    opts =>
                    {
                        //opts.DisableColors = true;
                    }));

            serviceCollection.AddLogging(builder => builder.AddDebug());
            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }
    }
}