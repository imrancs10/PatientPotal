//-----------------------------------------------------------------------
// <copyright file="ISurveyDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the ISurveyDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Defines a contract for survey DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ISurveyDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The active field.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The language field.</value>
        int LanguageId { get; set; }
      
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The flag for mapping</value>
        bool IsMapped { get; set; }

        /// <summary>
        /// Gets or sets the survey id and language id in encryption mode.
        /// </summary>
        /// <value>The survey id and language id.</value>
        string EncryptSurveyIdLanguageId { get; set; }

        IList<ISurveyQuestionMapDTO> ISurveyQuestionMapList { get; set; }

        /// <summary>
        /// Gets or sets the LanguageDTO.
        ///// </summary>
        ILanguageDTO ILanguageDTO { get; set; }
    }
}