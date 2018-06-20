//-----------------------------------------------------------------------
// <copyright file="IIntroductionBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IIntroductionBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Introduction Data BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IIntroductionBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Gets the introduction.
        /// </summary>
        /// <param name="id">Introduction id.</param>
        /// <returns>Introduction data component.</returns>
        OperationResult<IIntroductionDTO> GetIntroductionByLanguageId(int id);
    }
}