//-----------------------------------------------------------------------
// <copyright file="ITextInputTypeFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ITextInputTypeFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for text input type Facade.
    /// </summary>
    public interface ITextInputTypeFacade : IFacade
    {
        /// <summary>
        /// Gets the text input type list.
        /// </summary>
        /// <returns>List of text input type.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<ITextInputTypeDTO>> GetTextInputTypeList();
    }
}