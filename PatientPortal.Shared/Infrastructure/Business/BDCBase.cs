//-----------------------------------------------------------------------
// <copyright file="BDCBase.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the BDCBase class.</summary>
//-----------------------------------------------------------------------
namespace PatientPortal.Shared
{
    using PatientPortal.Shared.Infrastructure.Common.IoC;
    using PatientPortal.Shared.Infrastructure.Common.Logging;
    using System.Diagnostics.CodeAnalysis;
    using System;

    /// <summary>
    /// Represents the abstract base class for all Business Domain Components.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BDC",
        Justification = "abstract base class for all Business Domain Components")]
    public abstract class BDCBase : IBusinessDomainComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BDCBase"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "bdc",
            Justification = "Reviewed")]
        protected BDCBase()
        {
            this.Logger = ContainerProvider.Resolve<ILogger>();
        }

        /// <summary>
        /// Gets or sets creation date for the entity.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets date of modification for the entity.
        /// </summary>
        public DateTime ModifiedDate
        {
            get;
            set;
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