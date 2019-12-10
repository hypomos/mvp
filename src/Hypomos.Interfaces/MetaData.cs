namespace Hypomos.Interfaces
{
    using System;

    public class MetaData
    {
        DateTime LastModifyDateTimeUtc { get; }

        string Name { get; }

        long Size { get; }
    }
}