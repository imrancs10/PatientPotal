//-----------------------------------------------------------------------
// <copyright file="LanguageDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the LanguageDTO.cs file.</summary>
//-----------------------------------------------------------------------
 
namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for language.
    /// </summary>
    [Serializable]
    [DataContract]
    [EntityMapping("PatientPortal.Entities.Entities.Language", MappingType.TotalExplicit)]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class LanguageDTO : DTOBase, ILanguageDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageDTO"/> class.
        /// </summary>
        public LanguageDTO()
        {
        }

        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        /// <value>The language id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The name of language.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The culture code field.</value>
        [DataMember]
        public string CultureCode { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [DataMember]
        public override int ErrorCode { set; get; }
    }
}