//-----------------------------------------------------------------------
// <copyright file="IStateBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IStateBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for State BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IStateBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Get state list
        /// </summary>
        /// <returns>object of type IList<IStateDTO></returns>
        OperationResult<IList<IStateDTO>> GetStateList();
    }
}