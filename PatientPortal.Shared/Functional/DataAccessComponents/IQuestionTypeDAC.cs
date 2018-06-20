//-----------------------------------------------------------------------
// <copyright file="IQuestionTypeDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IQuestionTypeDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for question type DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IQuestionTypeDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get list of question type.
        /// </summary>
        /// <returns>List of question type data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<IQuestionTypeDTO> GetQuestionTypeList();
    }
}