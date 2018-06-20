//-----------------------------------------------------------------------
// <copyright file="IQuestionTypeDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the IQuestionTypeDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Defines a contract for question type DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IQuestionTypeDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the question type id.
        /// </summary>
        /// <value>The question type id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The question type.</value>
        string Type { get; set; }
    }
}