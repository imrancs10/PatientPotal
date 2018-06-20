//-----------------------------------------------------------------------
// <copyright file="IQuestionDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the IQuestionDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for survey DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IQuestionDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The type of Mandatary</value>
        bool IsMandatary { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The type of active</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The language id</value>
        int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text input type id</value>
        int? TextInputTypeId { get; set; }
        
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The question type id</value>
        int QuestionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The flag for mapping</value>
        bool IsMapped { get; set; }

        IList<IAnswerDTO> IAnswerDTOList { get; set; }

        IQuestionTypeDTO IQuestionTypeDTO { get; set; }

        ITextInputTypeDTO ITextInputTypeDTO { get; set; }

        /// <summary>
        /// Gets or sets the Language DTO.
        /// </summary>
        ILanguageDTO ILanguageDTO { get; set; }

    }
}