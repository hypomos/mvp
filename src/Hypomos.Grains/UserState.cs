namespace Hypomos.Grains
{
    using System.Collections.Generic;
    using Hypomos.Interfaces;
    using Hypomos.Interfaces.Models;

    public class UserState
    {
        public bool IsInitialized { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }

        public List<IMediaLibrary> MediaLibraries { get; set; }

        public List<StorageConfiguration> Storages { get; set; }
        
        public UserSetupState SetupState { get; set; }
    }
}