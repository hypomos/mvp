// Taken from: https://github.com/OrleansContrib/Orleans.Persistence.Minio
// MIT License

namespace Hypomos.Silo.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IMinioStorage
    {
        Task<bool> ContainerExits(string blobContainer);
        Task CreateContainerAsync(string blobContainer);
        Task<Stream> ReadBlob(string blobContainer, string blobName, string blobPrefix = null);

        Task UploadBlob(string blobContainer, string blobName, Stream blob, string blobPrefix = null,
            string contentType = null);

        Task DeleteBlob(string blobContainer, string blobName, string blobPrefix = null);
    }
}