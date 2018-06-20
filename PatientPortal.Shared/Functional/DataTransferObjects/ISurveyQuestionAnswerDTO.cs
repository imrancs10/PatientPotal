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
    /// Defines a contract for SurveyQuestionAnswer DTO.
    /// </summary>
    public interface ISurveyQuestionAnswerDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets SurveyId.
        /// </summary>
        /// <value>The SurveyId.</value>
        int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets QuestionId.
        /// </summary>
        /// <value>The QuestionId.</value>
        int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets AnswerId.
        /// </summary>
        /// <value>The AnswerId.</value>
        int? AnswerId { get; set; }

        /// <summary>
        /// Gets or sets TextInput.
        /// </summary>
        /// <value>The TextInput.</value>
        string TextInput { get; set; }

        /// <summary>
        /// Gets or sets Guid.
        /// </summary>
        /// <value>The Guid.</value>
        Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        /// <value>The UserId.</value>
        int? UserId { get; set; }

        /// <summary>
        /// Gets or sets CreatedDate.
        /// </summary>
        /// <value>The CreatedDate.</value>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets IQuestionDTO.
        /// </summary>
        /// <value>The IQuestionDTO.</value>
        IQuestionDTO IQuestionDTO { get; set; }

        /// <summary>
        /// Gets or sets ISurveyDTO.
        /// </summary>
        /// <value>The ISurveyDTO.</value>
        ISurveyDTO ISurveyDTO { get; set; }

        /// <summary>
        /// Gets or sets ProductId.
        /// </summary>
        /// <value>The ProductId.</value>
        int? ProductId { get; set; }

    }
}