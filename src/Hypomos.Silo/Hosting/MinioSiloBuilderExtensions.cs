// Taken from: https://github.com/OrleansContrib/Orleans.Persistence.Minio
// MIT License

namespace Hypomos.Silo.Hosting
{
    using System;
    using Hypomos.Silo.Storage;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Orleans;
    using Orleans.Hosting;
    using Orleans.Runtime;
    using Orleans.Storage;

    public static class MinioSiloBuilderExtensions
    {
        public static ISiloBuilder AddMinioGrainStorage(this ISiloBuilder builder, string providerName,
            Action<MinioGrainStorageOptions> options)
        {
            return builder.ConfigureServices(services =>
                services.AddMinioGrainStorage(providerName, ob => ob.Configure(options)));
        }

        public static IServiceCollection AddMinioGrainStorage(this IServiceCollection services, string providerName,
            Action<OptionsBuilder<MinioGrainStorageOptions>> options)
        {
            options?.Invoke(services.AddOptions<MinioGrainStorageOptions>(providerName));
            return services
                .AddSingletonNamedService(providerName, MinioGrainStorageFactory.Create)
                .AddSingletonNamedService(providerName,
                    (s, n) => (ILifecycleParticipant<ISiloLifecycle>) s.GetRequiredServiceByName<IGrainStorage>(n));
        }
    }
}