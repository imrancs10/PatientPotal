//-----------------------------------------------------------------------
// <copyright file="AdaptorBase.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the AdaptorBase.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using PatientPortal.Shared.Infrastructure.Common.Logging;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Represents abstract base class for integration adaptors.
    /// </summary>
    public abstract class AdaptorBase : IAdaptor
    {
        protected AdaptorBase()
        {
            
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptorBase"/> class.
        /// </summary>
        protected AdaptorBase(ILogger logger)
        {
            this.Logger = logger;
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
