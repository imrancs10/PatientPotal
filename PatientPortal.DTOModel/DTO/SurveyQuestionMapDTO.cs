//-----------------------------------------------------------------------
// <copyright file="SurveyQuestionMapDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the SurveyQuestionMapDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for SurveyQuestionMap.
    /// </summary>
    [Serializable]
    [DataContract]
    public class SurveyQuestionMapDTO : DTOBase, ISurveyQuestionMapDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyQuestionMapDTO"/> class.
        /// </summary>
        public SurveyQuestionMapDTO()
        {
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ErrorCode.
        /// </summary>
        /// <value>The ErrorCode.</value>
        public override int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the SurveyId.
        /// </summary>
        /// <value>The SurveyId.</value>
        [DataMember]
        public int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets the QuestionId.
        /// </summary>
        /// <value>The QuestionId.</value>
        [DataMember]
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the AnswerId.
        /// </summary>
        /// <value>The AnswerId.</value>
        [DataMember]
        public int AnswerId { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        /// <value>The ProductId.</value>
        [DataMember]
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ChildQuestionId.
        /// </summary>
        /// <value>The ChildQuestionId.</value>
        [DataMember]
        public int? ChildQuestionId { get; set; }

        /// <summary>
        /// Gets or sets the SurveyQuestionMapId.
        /// </summary>
        /// <value>The SurveyQuestionMapId.</value>
        [DataMember]
        public int? SurveyQuestionMapId { get; set; }

        /// <summary>
        /// Gets or sets the IsParent.
        /// </summary>
        /// <value>The IsParent.</value>
        [DataMember]
        public bool IsParent { get; set; }

        /// <summary>
        /// Gets or sets the QuestionOrderNumber.
        /// </summary>
        /// <value>The QuestionOrderNumber.</value>
        [DataMember]
        public int QuestionOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the IsMainNode.
        /// </summary>
        /// <value>The IsMainNode.</value>
        [DataMember]
        public int? IsMainNode { get; set; }

        /// <summary>
        /// Gets or sets the SameQuestionNumber.
        /// </summary>
        /// <value>The SameQuestionNumber.</value>
        [DataMember]
        public int? SameQuestionNumber { get; set; }

        /// <summary>
        /// Gets or sets the IsMainNode.
        /// </summary>
        /// <value>The IsMainNode.</value>
        [DataMember]
        public int? SameNodeNumber { get; set; }

        /// <summary>
        /// Gets or sets the IAnswerDTO.
        /// </summary>
        /// <value>The IAnswerDTO.</value>
        [DataMember]
        public IAnswerDTO IAnswerDTO { get; set; }

        /// <summary>
        /// Gets or sets the IProductDTO.
        /// </summary>
        /// <value>The IProductDTO.</value>
        [DataMember]
        public IProductDTO IProductDTO { get; set; }

        /// <summary>
        /// Gets or sets the IQuestionDTO.
        /// </summary>
        /// <value>The IQuestionDTO.</value>
        [DataMember]
        public IQuestionDTO IQuestionDTO { get; set; }

        /// <summary>
        /// Gets or sets the ISurveyDTO.
        /// </summary>
        /// <value>The ISurveyDTO.</value>
        [DataMember]
        public ISurveyDTO ISurveyDTO { get; set; }
    }
}