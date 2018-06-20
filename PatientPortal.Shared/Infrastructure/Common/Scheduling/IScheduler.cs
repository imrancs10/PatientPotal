//-----------------------------------------------------------------------
// <copyright file="IScheduler.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IScheduler class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared.Infrastructure.Common.Scheduling
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for scheduler.
    /// </summary>
    public interface IScheduler : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is initiated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initiated; otherwise, <c>false</c>.
        /// </value>
        /// TODO: Replace with SchedulerState Enum
        bool IsInitialized { get; }

        /// <summary>
        /// Initiates this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        void Terminate();
    }
}