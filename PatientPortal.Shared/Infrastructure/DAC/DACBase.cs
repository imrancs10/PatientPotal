//-----------------------------------------------------------------------
// <copyright file="DACBase.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the DACBase class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using PatientPortal.Shared.Infrastructure.Common.IoC;
    using PatientPortal.Shared.Infrastructure.Common.Logging;
using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents the abstract base class for data access components.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DAC",
        Justification = "Identifiers Should Be Cased Correctly")]
    public abstract class DACBase : IDataAccessComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DACBase" /> class.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dac",
            Justification = "Reviewed")]
        protected DACBase()
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