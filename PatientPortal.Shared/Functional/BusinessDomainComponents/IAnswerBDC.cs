//-----------------------------------------------------------------------
// <copyright file="IAnswerBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IAnswerBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Survey Data BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IAnswerBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Delete a answer record.
        /// </summary>
        /// <param name="id">Answer id.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IAnswerDTO> DeleteAnswerById(int id);
    }
}