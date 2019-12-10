namespace Hypomos.Interfaces
{
    using System.Threading.Tasks;
    using Orleans;

    /// <summary>
    /// key = identifier
    /// </summary>
    public interface IUserGrain : IGrainWithStringKey
    {
        Task<string> GetUsername();
    }
}