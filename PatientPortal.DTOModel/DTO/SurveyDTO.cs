//-----------------------------------------------------------------------
// <copyright file="SurveyDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the SurveyDTO.cs file.</summary>
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
    /// Represents the DTO class for survey.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class SurveyDTO : DTOBase, ISurveyDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyDTO"/> class.
        /// </summary>
        public SurveyDTO()
        {

            this.SurveyQuestionMapList = new List<SurveyQuestionMapDTO>();
            this.ISurveyQuestionMapList = new List<ISurveyQuestionMapDTO>();
            this.ILanguageDTO = new LanguageDTO();
        }

        /// <summary>
        /// Gets or sets the survey id.
        /// </summary>
        /// <value>The survey id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of survey.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The status field.</value>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language field.</value>
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
        /// Gets or sets the mapped flag.
        /// </summary>
        /// <value>The flag for mapping</value>
        [DataMember]
        public bool IsMapped { get; set; }

        /// <summary>
        /// Gets or sets the encrypted survey language ids.
        /// </summary>
        /// <value>The encrypted survey language ids.</value>
        [DataMember]
        public string EncryptSurveyIdLanguageId { get; set; }

        /// <summary>
        /// Gets or sets the SurveyQuestionMapDTO list.
        /// </summary>
        /// <value>The SurveyQuestionMapDTO.</value>
        [DataMember]
        public IList<SurveyQuestionMapDTO> SurveyQuestionMapList { get; set; }

        /// <summary>
        /// Gets or sets the IAnswerDTOList list.
        /// </summary>
        /// <value>The ISurveyQuestionMapList.</value>
        [DataMember]
        public IList<ISurveyQuestionMapDTO> ISurveyQuestionMapList { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The ErrorCode.</value>
        [DataMember]
        public override int ErrorCode { set; get; }

        /// <summary>
        /// Gets or sets the LanguageDTO.
        /// </summary>
        /// <value>The ILanguageDTO.</value>
        [DataMember]
        public ILanguageDTO ILanguageDTO { get; set; }
    }
}