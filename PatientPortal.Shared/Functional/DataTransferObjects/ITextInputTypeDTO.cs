//-----------------------------------------------------------------------
// <copyright file="ITextInputTypeDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the ITextInputTypeDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Defines a contract for text input type DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ITextInputTypeDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the text input type id.
        /// </summary>
        /// <value>The text input type id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text input type.</value>
        string InputType { get; set; }
    }
}