//-----------------------------------------------------------------------
// <copyright file="AnswerDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the AnswerDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for question.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class AnswerDTO : DTOBase, IAnswerDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerDTO"/> class.
        /// </summary>
        public AnswerDTO()
        {
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text of question id.</value>
        [DataMember]
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text of answer</value>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The image path of answer</value>
        [DataMember]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The tooltip for answer</value>
        [DataMember]
        public string ToolTip { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the Error Code.
        /// </summary>
        /// <value>The ErrorCode.</value>
        public override int ErrorCode { set; get; }
    }
}