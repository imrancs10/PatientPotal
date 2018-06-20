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
    public interface ISurveyQuestionMapDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the SurveyId.
        /// </summary>
        /// <value>The SurveyId.</value>
        int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets the QuestionId.
        /// </summary>
        /// <value>The QuestionId.</value>
        int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the AnswerId.
        /// </summary>
        /// <value>The AnswerId.</value>
        int AnswerId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The question type.</value>
        int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the child question id
        /// </summary>
        int? ChildQuestionId { get; set; }

        /// <summary>
        ///  Gets or sets the survey question map id
        /// </summary>
        int? SurveyQuestionMapId { get; set; }

        /// <summary>
        ///  Gets or sets the parent flag
        /// </summary>
        bool IsParent { get; set; }

        /// <summary>
        ///  Gets or sets the parent flag
        /// </summary>
        int? IsMainNode { get; set; }

        /// <summary>
        /// Gets or sets the IsMainNode.
        /// </summary>
        /// <value>The IsMainNode.</value>
        int? SameNodeNumber { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The question type.</value>
        int QuestionOrderNumber { get; set; }

                /// <summary>
        /// Gets or sets the SameQuestionNumber.
        /// </summary>
        /// <value>The SameQuestionNumber.</value>
        int? SameQuestionNumber { get; set; }

        #region Navigation Property

        IAnswerDTO IAnswerDTO { get; set; }

        /// <summary>
        /// Gets or sets the IProductDTO.
        /// </summary>
        /// <value>The IProductDTO.</value>
        IProductDTO IProductDTO { get; set; }

        /// <summary>
        /// Gets or sets the IQuestionDTO.
        /// </summary>
        /// <value>The IQuestionDTO.</value>
        IQuestionDTO IQuestionDTO { get; set; }

        /// <summary>
        /// Gets or sets the ISurveyDTO.
        /// </summary>
        /// <value>The ISurveyDTO.</value>
        ISurveyDTO ISurveyDTO { get; set; }
        #endregion

    }
}