namespace Hypomos.Grains
{
    using System.Threading.Tasks;
    using Hypomos.Interfaces;
    using Orleans;

    public class UserGrain : Grain<UserState>, IUserGrain
    {
        public Task<string> GetUsername()
        {
            return Task.FromResult(this.State.Username);
        }
    }

    public class UserState
    {
        public string Username { get; set; }
    }
}
