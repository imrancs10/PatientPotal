//-----------------------------------------------------------------------
// <copyright file="ITerritoryEmailDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ITerritoryEmailDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for ITerritoryEmail DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ITerritoryEmailDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get Territory email on state wise
        /// </summary>
        /// <param name="StateId">object of type int</param>
        /// <returns>object of type IList<ITerritoriesEmailDTO></returns>
        IList<ITerritoriesEmailDTO> GetTerritoryEmails(int StateId);

    }
}