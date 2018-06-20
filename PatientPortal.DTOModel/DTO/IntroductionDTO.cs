//-----------------------------------------------------------------------
// <copyright file="IntroductionDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IntroductionDTO.cs file.</summary>
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
    /// Represents the DTO class for introduction.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class IntroductionDTO : DTOBase, IIntroductionDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntroductionDTO"/> class.
        /// </summary>
        public IntroductionDTO()
        {
        }

        /// <summary>
        /// Gets or sets the introduction id.
        /// </summary>
        /// <value>The introduction id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        /// <value>The id of language.</value>
        [DataMember]
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The type of Description</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>The type of Image</value>
        [DataMember]
        public string ImagePath { get; set; }

        /// <summary>
        /// Get or sets error code
        /// </summary>
        [DataMember]
        public override int ErrorCode { get; set; }
    }
}