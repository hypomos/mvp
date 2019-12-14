// Taken from: https://github.com/OrleansContrib/Orleans.Persistence.Minio
// MIT License
namespace Orleans.Persistence.Minio
{
    public class MinioGrainStorageOptions
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Endpoint { get; set; }
        public string Container { get; set; }
    }
}
