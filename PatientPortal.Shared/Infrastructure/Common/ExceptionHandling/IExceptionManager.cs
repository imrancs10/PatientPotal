//-----------------------------------------------------------------------
// <copyright file="IExceptionManager.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IExceptionManager class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for exception manager/handler.
    /// </summary>
    public interface IExceptionManager
    {
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void HandleException(Exception exception);

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalMessage">The additional message.</param>
        void HandleException(Exception exception, string additionalMessage);

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalMessage">The additional message.</param>
        void HandleException(string additionalMessage);
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalMessage">The additional message.</param>
        /// <param name="sendNotification">If set to <c>true</c> send notification.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Done intentially.")]
        void HandleException(Exception exception, string additionalMessage = "", bool sendNotification = false);
    }
}