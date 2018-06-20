//-----------------------------------------------------------------------
// <copyright file="ITextInputTypeDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ITextInputTypeDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for text input type DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ITextInputTypeDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get list of question type.
        /// </summary>
        /// <returns>List of question type data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<ITextInputTypeDTO> GetTextInputTypeList();
    }
}