namespace Hypomos.Interfaces
{
    using System.Threading.Tasks;

    public interface IMinioStorageProvider : IStorageProvider
    {
        Task SetConfiguration(MinioConfiguration config);

        Task Scan();
    }
}