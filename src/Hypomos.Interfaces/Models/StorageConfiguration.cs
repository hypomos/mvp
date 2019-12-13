namespace Hypomos.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    public class StorageConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public StorageClass StorageClass { get; set; }

        public StorageType StorageType { get; set; }

        public Dictionary<string,string> Settings { get; set; }
    }
}