namespace Hypomos.Interfaces
{
    /// <summary>
    /// This interface contains all relevant information for a scan context (e.g. User, StorageProvider, ...)
    /// </summary>
    public interface IScanContext
    {
        IUserGrain User { get; }
    }
}