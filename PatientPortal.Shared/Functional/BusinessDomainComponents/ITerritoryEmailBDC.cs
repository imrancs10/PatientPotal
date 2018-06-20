//-----------------------------------------------------------------------
// <copyright file="ITerritoryEmailBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ITerritoryEmailBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for TerritoryEmail BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ITerritoryEmailBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Get Territoryn email on state wise
        /// </summary>
        /// <param name="StateId">object of type int</param>
        /// <returns>object of type IList<ITerritoriesEmailDTO></returns>
        OperationResult<IList<ITerritoriesEmailDTO>> GetTerritoryEmails(int StateId);

    }
}