namespace Hypomos.Interfaces
{
    using System.Threading.Tasks;
    using Hypomos.Interfaces.Models;

    public interface IMinioStorageProviderGrain : IStorageProviderGrain
    {
        Task SetConfiguration(MinioConfiguration config);

        Task Scan(string username);
    }
}