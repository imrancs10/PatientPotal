//-----------------------------------------------------------------------
// <copyright file="SurveyQuestionAnswerDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the SurveyQuestionAnswerDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for SurveyQuestionAnswer.
    /// </summary>
    [Serializable]
    [DataContract]
    public class SurveyQuestionAnswerDTO : DTOBase, ISurveyQuestionAnswerDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyQuestionAnswerDTO"/> class.
        /// </summary>
        public SurveyQuestionAnswerDTO()
        {
            this.IQuestionDTO = new QuestionDTO();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets ErrorCode.
        /// </summary>
        /// <value>The ErrorCode.</value>
        public override int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets SurveyId.
        /// </summary>
        /// <value>The SurveyId.</value>
        [DataMember]
        public int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets QuestionId.
        /// </summary>
        /// <value>The QuestionId.</value>
        [DataMember]
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets AnswerId.
        /// </summary>
        /// <value>The AnswerId.</value>
        [DataMember]
        public int? AnswerId { get; set; }

        /// <summary>
        /// Gets or sets TextInput.
        /// </summary>
        /// <value>The TextInput.</value>
        [DataMember]
        public string TextInput { get; set; }

        /// <summary>
        /// Gets or sets Guid.
        /// </summary>
        /// <value>The Guid.</value>
        [DataMember]
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        /// <value>The UserId.</value>
        [DataMember]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets CreatedDate.
        /// </summary>
        /// <value>The CreatedDate.</value>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets ModifiedDate.
        /// </summary>
        /// <value>The ModifiedDate.</value>
        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets IQuestionDTO.
        /// </summary>
        /// <value>The IQuestionDTO.</value>
        [DataMember]
        public IQuestionDTO IQuestionDTO { get; set; }

        /// <summary>
        /// Gets or sets ISurveyDTO.
        /// </summary>
        /// <value>The ISurveyDTO.</value>
        [DataMember]
        public ISurveyDTO ISurveyDTO { get; set; }

        /// <summary>
        /// Gets or sets AnswerIds.
        /// </summary>
        /// <value>The AnswerIds.</value>
        [DataMember]
        public string AnswerIds { get; set; }

         [DataMember]
        public int? ProductId { get; set; }
    }
}