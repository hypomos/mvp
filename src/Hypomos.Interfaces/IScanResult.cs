namespace Hypomos.Interfaces
{
    using System;

    /// <summary>
    /// This interface represents a simple "file"
    /// </summary>
    public interface IScanResult
    {
        DateTime LastModifyDateTimeUtc { get; }

        string Name { get; }

        IScanContext ScanContext { get; }

        long Size { get; }
    }
}