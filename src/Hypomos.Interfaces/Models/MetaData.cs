namespace Hypomos.Interfaces.Models
{
    using System;

    public class MetaData
    {
        public DateTime LastModifyDateTimeUtc { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }
    }
}