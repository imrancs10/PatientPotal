//-----------------------------------------------------------------------
// <copyright file="ExceptionManager.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ExceptionManager class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using PatientPortal.Shared.Infrastructure.Common.Logging;
    using System;
    using System.Text;

    /// <summary>
    /// Represents manager class to handle exceptions.
    /// </summary>
    public sealed class ExceptionManager : IExceptionManager
    {
        /// <summary>
        /// Holds the instance of logger.
        /// </summary>
        private ILogger logger;

        public ExceptionManager()
        {
          
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionManager"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ExceptionManager(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void HandleException(Exception exception)
        {
            this.HandleException(exception, string.Empty, false);
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalMessage">The additionalMessage.</param>
        public void HandleException(Exception exception, string additionalMessage)
        {
            this.HandleException(exception, additionalMessage, false);
        }

        
        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalMessage">The additional message.</param>
        /// <param name="sendNotification">If set to <c>true</c> send notification.</param>
        public void HandleException(Exception exception, string additionalMessage, bool sendNotification)
        {
            this.logger.LogError(exception);
            if (!string.IsNullOrEmpty(additionalMessage))
            {
                this.logger.LogError(additionalMessage);
            }

            if (sendNotification)
            {
                SendExceptionEmail(FormatException(exception));
            }
        }

        /// Handles the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalMessage">The additional message.</param>
        /// <param name="sendNotification">If set to <c>true</c> send notification.</param>
        public void HandleException(string additionalMessage)
        { 
                this.logger.LogDebug(additionalMessage);
        }

        /// <summary>
        /// Sends the exception email.
        /// </summary>
        /// <param name="message">The message.</param>
        private static void SendExceptionEmail(string message)
        {
            // TODO: Send mail
            throw new NotImplementedException(
                string.Format(System.Globalization.CultureInfo.InvariantCulture, "Send Exception email is not implemented ! Message: {0}", message));
        }

        /// <summary>
        /// Formats the exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>Format Exception.</returns>
        private static string FormatException(Exception ex)
        {
            var message = new StringBuilder();

            // TODO: build the message here
            message.Append(ex.Message);

            return message.ToString();
        }
    }
}