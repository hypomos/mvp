namespace Hypomos.Interfaces.Models
{
    using System;

    public class ScanEventData
    {
        public string ETag { get; set; }
        public bool IsDir { get; set; }
        public string Key { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public string LastModified { get; set; }
    }
}