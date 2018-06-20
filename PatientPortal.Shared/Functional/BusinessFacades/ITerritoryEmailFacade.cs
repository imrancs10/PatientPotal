//-----------------------------------------------------------------------
// <copyright file="ITerritoryEmailFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ITerritoryEmailFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for TerritoryEmailFacade.
    /// </summary>
    public interface ITerritoryEmailFacade : IFacade
    {
        /// <summary>
        /// Get Territoryn email on state wise
        /// </summary>
        /// <param name="StateId">object of type int</param>
        /// <returns>object of type IList<ITerritoriesEmailDTO></returns>
        OperationResult<IList<ITerritoriesEmailDTO>> GetTerritoryEmails(int StateId);
    }
}