namespace Hypomos.Web.Cluster
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Orleans;
    using Hypomos.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Runtime;

    public class ClusterClientHostedService : Microsoft.Extensions.Hosting.IHostedService
    {
        private readonly ILogger<ClusterClientHostedService> logger;

        public ClusterClientHostedService(ILogger<ClusterClientHostedService> logger, ILoggerProvider loggerProvider, IConfiguration config)
        {
            this.logger = logger;
            this.Client = new ClientBuilder()
                .AddSimpleMessageStreamProvider(Constants.SmsProvider)
                .ConfigureAppConfiguration(builder => builder.AddConfiguration(config))
                .Configure<ClusterOptions>(
                    options => config.GetSection("Orleans").Bind(options))

#if DEBUG
                .UseLocalhostClustering()
#else
                .UseKubeGatewayListProvider()
#endif
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(IUserGrain).Assembly).WithReferences();
                })
                .ConfigureLogging(builder => builder.AddProvider(loggerProvider))
                .Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var attempt = 0;
            var maxAttempts = 100;
            var delay = TimeSpan.FromSeconds(1);
            return this.Client.Connect(async error =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                if (++attempt < maxAttempts)
                {
                    this.logger.LogWarning(error,
                        "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                        attempt, maxAttempts);

                    try
                    {
                        await Task.Delay(delay, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    this.logger.LogError(error,
                        "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                        attempt, maxAttempts);

                    return false;
                }
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await this.Client.Close();
            }
            catch (OrleansException error)
            {
                this.logger.LogWarning(error, "Error while gracefully disconnecting from Orleans cluster. Will ignore and continue to shutdown.");
            }
        }

        public IClusterClient Client { get; }
    }
}