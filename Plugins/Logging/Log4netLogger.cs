//-----------------------------------------------------------------------
// <copyright file="Log4netLogger.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the Log4netLogger class.</summary>
//-----------------------------------------------------------------------

using System;
using PatientPortal.Shared;
using PatientPortal.Shared.Infrastructure.Common.Logging;

namespace PatientPortal.Plugin.Log4net
{

    /// <summary>
    /// Used for representing logging operations.
    /// </summary>
    public class Log4netLogger : Logger
    {
        #region Private variables
        /// <summary>
        /// Logging instance variable.
        /// </summary>
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Log4netLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
            // TODO : Read these settings based on NLog configuration.
            base.IsErrorEnabled = Convert.ToBoolean(PatientPortalConstants.ConfigurationKeys.errorLog);
            this.IsDebugEnabled = Convert.ToBoolean(PatientPortalConstants.ConfigurationKeys.debugLog);
            base.IsInfoEnabled = Convert.ToBoolean(PatientPortalConstants.ConfigurationKeys.infoLog);
            base.IsWarningEnabled = Convert.ToBoolean(PatientPortalConstants.ConfigurationKeys.warningLog);
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Called when [log debug].
        /// </summary>
        /// <param name="message">The message.</param>
        protected override void OnLogDebug(object message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Called when [log error].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="logLevel">The log level.</param>
        protected override void OnLogError(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Called when [log error].
        /// </summary>
        /// <param name="exceptionToLog">The exception to log.</param>
        protected override void OnLogError(Exception exceptionToLog)
        {
            _logger.Error(exceptionToLog);
        }

        /// <summary>
        /// Called when [log error].
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        protected override void OnLogError(object message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        /// <summary>
        /// Called when [log info].
        /// </summary>
        /// <param name="message">The message.</param>
        protected override void OnLogInfo(object message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Called when [log warning].
        /// </summary>
        /// <param name="message">The message.</param>
        protected override void OnLogWarning(object message)
        {
            _logger.Info(message);
        }
        #endregion
    }
}
