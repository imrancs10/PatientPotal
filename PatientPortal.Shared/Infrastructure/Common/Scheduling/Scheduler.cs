//-----------------------------------------------------------------------
// <copyright file="IScheduler.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IScheduler class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared.Infrastructure.Common.Scheduling
{
    using System;

    /// <summary>
    /// Represents the abstract base class for all Scheduler implementations.
    /// </summary>
    public abstract class Scheduler : IScheduler
    {       
        /// <summary>
        /// Gets a value indicating whether this instance is initiated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initiated; otherwise, <c>false</c>.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsInitialized { get; protected set; }

        /// <summary>
        /// Initiates this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Start()
        {
            if (IsInitialized)
            {
                this.OnStart();
            }
        }

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Terminate()
        {
            if (IsInitialized)
            {
                this.OnTerminate();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            this.OnDispose();
        }

        #region Protected Members
        /// <summary>
        /// Called when [start].
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void OnStart()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when [terminate].
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void OnTerminate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when [dispose].
        /// </summary>
        protected virtual void OnDispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
