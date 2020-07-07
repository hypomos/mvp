namespace Hypomos.GrainInterfaces
{
    using System.Threading.Tasks;

    using Hypomos.GrainInterfaces.Models;

    public interface IMinioStorageProviderGrain : IStorageProviderGrain
    {
        Task SetConfiguration(MinioConfiguration config);

        Task Scan(string username);
    }
}