//-----------------------------------------------------------------------
// <copyright file="IIntroductionDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IIntroductionDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Introduction DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IIntroductionDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get introduction data by language id.
        /// </summary>
        /// <param name="id">Language id.</param>
        /// <returns>Introduction data component.</returns>
        IIntroductionDTO GetIntroductionByLanguageId(int id);
    }
}