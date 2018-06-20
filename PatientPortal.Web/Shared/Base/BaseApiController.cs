//-----------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the BaseApiController class.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PatientPortal.Shared;
using PatientPortal.Shared.Infrastructure.Common.Logging;
using System.Web.Mvc;
using PatientPortal.Shared.Infrastructure.Common.IoC;
using System.Reflection;

namespace PatientPortal.Web
{
    /// <summary>
    /// Represents the abstract base class for all controllers.
    /// </summary>
    public class BaseApiController : ApiController
    {
        #region Ctor
        /// <summary>
        /// Default Constructor
        /// </summary>
        protected BaseApiController() { }
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        protected BaseApiController(IExceptionManager exceptionManager)
        {
            this.ExceptionManager = exceptionManager;
            this.Logger = ContainerProvider.Resolve<ILogger>();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="logger">The logger.</param>
        protected BaseApiController(IExceptionManager exceptionManager, ILogger logger)
        {
            this.ExceptionManager = exceptionManager;
            this.Logger = logger;
        }
        #endregion

        #region Private Member
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; private set; }
        #endregion

        #region Protected Member
        /// <summary>
        /// Gets or sets the exception manager.
        /// </summary>
        /// <value> The exception manager. </value>
        protected IExceptionManager ExceptionManager { get; set; }

        /// <summary>
        /// On Exception event handler.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected override System.Web.Http.Results.ExceptionResult InternalServerError(Exception exception)
        {
            this.ExceptionManager.HandleException(exception);
            return base.InternalServerError(exception);
        }

        #endregion

    }
}



