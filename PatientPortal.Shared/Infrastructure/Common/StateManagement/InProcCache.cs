//-----------------------------------------------------------------------
// <copyright file="InProcCache.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the InProcCache class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared.Infrastructure.Common.StateManagement
{
    using System;
    using System.Runtime.Caching;

    /// <summary>
    /// Represents the in-proc (in-memory) cache item.
    /// </summary>
    public sealed class InProcCache: State
    {
        /// <summary>
        /// Puts the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="stateItem">The state item.</param>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        protected override void PutItem<T>(string key, T stateItem, TimeSpan slidingExpiration)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            if (slidingExpiration != null)
            {
                cacheItemPolicy.SlidingExpiration = slidingExpiration;
            }

            MemoryCache.Default.Add(key, stateItem, cacheItemPolicy); 
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override T GetItem<T>(string key)
        {
            return (T)MemoryCache.Default.Get(key);
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        protected override void RemoveItem<T>(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        /// <summary>
        /// Clears the state.
        /// </summary>
        protected override  void ClearState()
        {
            MemoryCache.Default.Dispose();
        }         
    }
}
