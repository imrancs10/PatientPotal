//-----------------------------------------------------------------------
// <copyright file="QuestionDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the QuestionDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the DTO class for question.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class QuestionDTO : DTOBase, IQuestionDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionDTO"/> class.
        /// </summary>
        public QuestionDTO()
        {
            this.AnswerDTOList = new List<AnswerDTO>();
            this.QuestionTypeDTO = new QuestionTypeDTO();
            this.TextInputTypeDTO = new TextInputTypeDTO();
            this.IAnswerDTOList = new List<IAnswerDTO>();
            this.ILanguageDTO = new  LanguageDTO();
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
        /// <value>The text of question.</value>
        [DataMember]
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The type of Mandatary</value>
        [DataMember]
        public bool IsMandatary { get; set; }
        
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The type of active</value>
        [DataMember]
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The question type id</value>
        [DataMember]
        public int QuestionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text input type id</value>
        [DataMember]
        public int? TextInputTypeId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The language id</value>
        [DataMember]
        public int LanguageId { get; set; }
        
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
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The flag for mapping</value>
        [DataMember]
        public bool IsMapped { get; set; }

        /// <summary>
        /// Gets or sets the AnswerDTO list.
        /// </summary>
        [DataMember]
        public IList<AnswerDTO> AnswerDTOList { get; set; }

        /// <summary>
        /// Gets or sets the AnswerDTO list.
        /// </summary>
        [DataMember]
        public IList<IAnswerDTO> IAnswerDTOList { get; set; }

        /// <summary>
        /// Gets or sets the QuestionTypeDTO list.
        /// </summary>
        [DataMember]
        public QuestionTypeDTO QuestionTypeDTO { get; set; }

        /// <summary>
        /// Gets or sets the QuestionTypeDTO list.
        /// </summary>
        [DataMember]
        public TextInputTypeDTO TextInputTypeDTO { get; set; }

        /// <summary>
        /// Get or sets error code
        /// </summary>
        [DataMember]
        public override int ErrorCode { get; set; }

        /// <summary>
        /// Get or sets IQuestionTypeDTO
        /// </summary>
        [DataMember]
        public IQuestionTypeDTO IQuestionTypeDTO { get; set; }

        /// <summary>
        /// Get or sets ITextInputTypeDTO
        /// </summary>
        [DataMember]
        public ITextInputTypeDTO ITextInputTypeDTO { get; set; }
         
        /// <summary>
        /// Gets or sets the Language DTO.
        /// </summary>
        [DataMember]
        public ILanguageDTO ILanguageDTO { get; set; }
    }
}