//-----------------------------------------------------------------------
// <copyright file="IState.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IState class.</summary>
//-----------------------------------------------------------------------

using System;
namespace PatientPortal.Shared.Infrastructure.Common.StateManagement
{
    /// <summary>
    /// Defines the contract for state (in-proc or out-proc cache, session, etc.) to manage state items.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Puts (adds or replaces) the state item in the State store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stateItem">The state entity.</param>
        void Put<T>(T stateItem) where T : IStateItem;

        /// <summary>
        /// Puts (adds or replaces) the state item in the State store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        void Put<T>(string key, T stateItem) where T : IStateItem;

        /// <summary>
        /// Puts (adds or replaces) the state item in the State store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stateItem">The state entity.</param>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        void Put<T>(T stateItem, TimeSpan slidingExpiration) where T : IStateItem;

        /// <summary>
        /// Puts (adds or replaces) the state item in the State store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="stateItem">The state entity.</param>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        void Put<T>(string key, T stateItem, TimeSpan slidingExpiration) where T : IStateItem;

        /// <summary>
        /// Gets the state item from the State store.
        /// </summary>
        /// <returns></returns>
        T Get<T>() where T: IStateItem;

        /// <summary>
        /// Gets the state item from the State store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Get<T>(string key) where T : IStateItem;

        /// <summary>
        /// Removes the state item from the State store.
        /// </summary>
        void Remove<T>(string key) where T : IStateItem;

        /// <summary>
        /// Removes the state item from the State store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Remove<T>() where T : IStateItem;

        /// <summary>
        /// Cleares the entire State of all state items.
        /// </summary>
        void Clear();                    
    }
}
