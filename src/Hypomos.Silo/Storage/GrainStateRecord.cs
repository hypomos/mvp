// Taken from: https://github.com/OrleansContrib/Orleans.Persistence.Minio
// MIT License

namespace Orleans.Persistence.Minio.Storage
{
    public partial class MinioGrainStorage
    {
        public class GrainStateRecord
        {
            public int ETag { get; set; }
            public object State { get; set; }
        }
    }
}
