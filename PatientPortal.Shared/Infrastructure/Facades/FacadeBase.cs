//-----------------------------------------------------------------------
// <copyright file="FacadeBase.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the FacadeBase class.</summary>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using PatientPortal.Shared.Infrastructure.Common.IoC;
using PatientPortal.Shared.Infrastructure.Common.Logging;
namespace PatientPortal.Shared
{
    /// <summary>
    /// Represents the abstract base class for all facades.
    /// </summary>
    public abstract class FacadeBase : IFacade
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacadeBase"/> class.
        /// </summary>
        protected FacadeBase()
        {
            this.Logger = ContainerProvider.Resolve<ILogger>();
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; private set; }

    }
}