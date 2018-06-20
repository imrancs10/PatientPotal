//-----------------------------------------------------------------------
// <copyright file="State.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the State class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared.Infrastructure.Common.StateManagement
{
    using System;

    /// <summary>
    /// Represents the abstract base class for state (in-proc or out-proc cache, session, etc.) to manage state entities.
    /// </summary>
    public abstract class State: IState
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        protected State()
        {
        }
        #endregion

        #region IState Implementation
        /// <summary>
        /// Puts the specified state item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stateItem">The state item.</param>
        public void Put<T>(T stateItem) where T : IStateItem
        {
            this.PutItem<T>(this.GetDefaultKey<T>(), stateItem);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="stateItem">The state item.</param>
        public void Put<T>(string key, T stateItem) where T : IStateItem
        {
            this.PutItem<T>(key, stateItem);
        }

        /// <summary>
        /// Puts the specified state item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stateItem">The state item.</param>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        public void Put<T>(T stateItem, TimeSpan slidingExpiration) where T : IStateItem
        {
            this.PutItem<T>(this.GetDefaultKey<T>(), stateItem, slidingExpiration);
        }

        /// <summary>
        /// Puts the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="stateItem">The state item.</param>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        public void Put<T>(string key, T stateItem, TimeSpan slidingExpiration) where T : IStateItem
        {
            this.PutItem<T>(key, stateItem, slidingExpiration);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : IStateItem
        {
            return this.GetItem<T>(this.GetDefaultKey<T>());
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : IStateItem
        {
            return this.GetItem<T>(key);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        public void Remove<T>(string key) where T : IStateItem
        {
            this.RemoveItem<T>(key);
        }

        /// <summary>
        /// Removes this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>() where T : IStateItem
        {
            this.RemoveItem<T>(this.GetDefaultKey<T>());
        }

        /// <summary>
        /// Cleares the entire State of all state items.
        /// </summary>
        public void Clear()
        {
            this.ClearState();
        }      
        #endregion   
        
        /// <summary>
        /// Puts the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="stateItem">The state item.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void PutItem<T>(string key, T stateItem) where T : IStateItem
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Puts the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="stateItem">The state item.</param>
        /// <param name="slidingExpiration">The sliding expiration.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void PutItem<T>(string key, T stateItem, TimeSpan slidingExpiration) where T : IStateItem
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual T GetItem<T>(string key) where T : IStateItem
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void RemoveItem<T>(string key) where T : IStateItem
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the state.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void ClearState()
        {
            throw new NotImplementedException();
        }         

        #region Helper methods
        /// <summary>
        /// Gets the default key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetDefaultKey<T>()
        {
            //Option 1 and 2
            //Type type = typeof(T);

            //Option 1
            //return type.ToString() + "_Default";
            return typeof(T).ToString() + "_Default";

            //Option 2
            //return type.Name + "_" + type.GUID.ToString();
        } 
        #endregion
    }
}