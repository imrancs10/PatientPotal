//-----------------------------------------------------------------------
// <copyright file="ITextInputTypeBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ITextInputTypeBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for text input type BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ITextInputTypeBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Gets the text input type list.
        /// </summary>
        /// <returns>List of text input type component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<ITextInputTypeDTO>> GetTextInputTypeList();
    }
}