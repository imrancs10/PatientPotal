//-----------------------------------------------------------------------
// <copyright file="PastSurveyDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the PastSurveyDTO.cs file.</summary>
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
    /// Represents the DTO class for PastSurvey.
    /// </summary>
    [Serializable]
    [DataContract]
    public class PastSurveyDTO : DTOBase, IPastSurveyDTO
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public PastSurveyDTO()
        {
            RecommendationProducts = new List<ProductDTO>();
        }

        /// <summary>
        /// Gets or sets the Past Survey id.
        /// </summary>
        [DataMember]
        public int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets the SurveyName.
        /// </summary>
        [DataMember]
        public string SurveyName { get; set; }

        /// <summary>
        /// Gets or sets the SurveyTakenDate.
        /// </summary>
        [DataMember]
        public DateTime SurveyTakenDate { get; set; }

        /// <summary>
        /// Gets or sets the Recommendation Product.
        /// </summary>
        [DataMember]
        public List<ProductDTO> RecommendationProducts { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [DataMember]
        public override int ErrorCode { set; get; }

        /// <summary>
        /// Gets or sets the Guid.
        /// </summary>
        [DataMember]
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the Recommendations.
        /// </summary>
        [DataMember]
        public string Recommendations { get; set; }

        /// <summary>
        /// Gets or sets the SurveyTakenDateDisplay.
        /// </summary>
        [DataMember]
        public string SurveyTakenDateDisplay { get; set; }

        /// <summary>
        /// Gets or sets the encrypted survey language ids.
        /// </summary>
        /// <value>The encrypted survey language ids.</value>
        [DataMember]
        public string EncryptSurveyIdLanguageId { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language field.</value>
        [DataMember]
        public int LanguageId { get; set; }

    }
}