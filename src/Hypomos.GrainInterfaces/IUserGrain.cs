﻿namespace Hypomos.GrainInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Hypomos.GrainInterfaces.Models;

    using Orleans;

    /// <summary>
    ///     key = identifier
    /// </summary>
    public interface IUserGrain : IGrainWithStringKey
    {
        Task<UserData> GetPersonalDetails();

        Task SetPersonalDetails(UserPersonalDetails personalDetails);

        Task<IEnumerable<StorageConfiguration>> GetStorageProviders();

        Task AddStorageProvider(StorageConfiguration configuration);
        
        //Task RemoveStorageProvider(StorageConfiguration configuration);
    }
}