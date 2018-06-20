//-----------------------------------------------------------------------
// <copyright file="IAnswerDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the IAnswerDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Defines a contract for answer DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IAnswerDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The user id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The image path of answer</value>
        string ImagePath { get; set; }
              
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The tooltip for answer</value>
        string ToolTip { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        DateTime? ModifiedDate { get; set; }
    }
}