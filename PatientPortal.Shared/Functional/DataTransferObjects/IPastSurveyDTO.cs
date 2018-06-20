//-----------------------------------------------------------------------
// <copyright file="ILanguageDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary> This is the IPastSurveyDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Past Survey DTO.
    /// </summary>
    public interface IPastSurveyDTO : IDTO
    {

        /// <summary>
        /// Gets or sets the Past Survey id.
        /// </summary>
        int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets the SurveyName.
        /// </summary>
        string SurveyName { get; set; }

        /// <summary>
        /// Gets or sets the SurveyTakenDate.
        /// </summary>
        DateTime SurveyTakenDate { get; set; }

        /// <summary>
        /// Gets or sets the SurveyTakenDateDisplay.
        /// </summary>
        string SurveyTakenDateDisplay { get; set; }

        /// <summary>
        /// Gets or sets the recommendations.
        /// </summary>
        string Recommendations { get; set; }

        /// <summary>
        /// Gets or sets the encrypted survey language ids.
        /// </summary>
        /// <value>The encrypted survey language ids.</value>
        string EncryptSurveyIdLanguageId { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language field.</value>
        int LanguageId { get; set; }
    }
}