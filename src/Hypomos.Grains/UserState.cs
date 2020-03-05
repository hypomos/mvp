namespace Hypomos.Grains
{
    using System.Collections.Generic;
    using Hypomos.Interfaces;

    public class UserState
    {
        public bool IsInitialized { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }

        public List<IMediaLibrary> MediaLibraries { get; set; }
    }
}