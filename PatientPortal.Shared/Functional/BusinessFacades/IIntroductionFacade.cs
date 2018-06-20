//-----------------------------------------------------------------------
// <copyright file="IIntroductionFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IIntroductionFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for introduction Facade.
    /// </summary>
    public interface IIntroductionFacade : IFacade
    {
        /// <summary>
        /// Gets the introduction by language id.
        /// </summary>
        /// <param name="id">language id.</param>
        /// <returns>Introduction data.</returns>
        OperationResult<IIntroductionDTO> GetIntroductionByLanguageId(int id);
    }
}