//-----------------------------------------------------------------------
// <copyright file="IStateFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IStateFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for StateFacade.
    /// </summary>
    public interface IStateFacade : IFacade
    {
        /// <summary>
        /// Get state list
        /// </summary>
        /// <returns>object of type IList<IStateDTO></returns>
        OperationResult<IList<IStateDTO>> GetStateList();
    }
}