namespace Hypomos.Interfaces.Models
{
    public class MinioConfiguration
    {
        public string Endpoint { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public bool WithSsl { get; set; }

        public string BucketName { get; set; }

        public string StorageKind { get; set; }
    }
}