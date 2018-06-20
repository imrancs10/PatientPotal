//-----------------------------------------------------------------------
// <copyright file="OutOfProcCache.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the OutOfProcCache class.</summary>
//-----------------------------------------------------------------------

namespace YaleNexTouch.Plugin.StateManagement.AppFabric
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.ApplicationServer.Caching;
    using System;
    using YaleNexTouch.Shared.Infrastructure.Common.StateManagement;

    /// <summary>
    /// Represents the out-proc cache item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OutOfProcCache : State
    {
        /// <summary>
        /// The _cache
        /// </summary>
        private readonly DataCache dataCache;

        /// <summary>
        /// The app fabric cache server name
        /// </summary>
        private static readonly string AppFabricCacheServerName = "localhost"; //ConfigurationSettings.AppFabricCacheServerName;

        /// <summary>
        /// The _app fabric cache server port number
        /// </summary>
        private static readonly int AppFabricCacheServerPortNumber = 22233; //ConfigurationSettings.AppFabricCachePortNumber;

        /// <summary>
        /// The _app fabric cache name
        /// </summary>
        private static readonly string AppFabricCacheName = "default"; //ConfigurationSettings.AppFabricCacheName;

        /// <summary>
        /// Initializes a new instance of the DistributedCache class.
        /// </summary>
        public OutOfProcCache()
        {
            dataCache = new DataCacheFactory().GetDefaultCache();

            //var servers = new List<DataCacheServerEndpoint>();
            //servers.Add(new DataCacheServerEndpoint(AppFabricCacheServerName, AppFabricCacheServerPortNumber));

            //var configuration = new DataCacheFactoryConfiguration();
            //configuration.Servers = servers;
            //configuration.LocalCacheProperties = new DataCacheLocalCacheProperties();

            //DataCacheSecurity security = new DataCacheSecurity(DataCacheSecurityMode.None, DataCacheProtectionLevel.None);
            //configuration.SecurityProperties = security;

            //DataCacheClientLogManager.ChangeLogLevel(TraceLevel.Off);
            //dataCache = new DataCacheFactory(configuration).GetCache(AppFabricCacheName);
        }

        /// <summary>
        /// Puts the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="stateItem">The state item.</param>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void PutItem<T>(string key, T stateItem, TimeSpan slidingExpiration)
        {
            dataCache.Put(key, stateItem, slidingExpiration); 
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override T GetItem<T>(string key)
        {
            return (T)dataCache.Get(key);
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void RemoveItem<T>(string key)
        {
            dataCache.Remove(key);
        }

        /// <summary>
        /// Clears the state.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void ClearState()
        {
            foreach (string regionName in dataCache.GetSystemRegions()) 
            {
                dataCache.ClearRegion(regionName);
            } 
        }
    }
}
