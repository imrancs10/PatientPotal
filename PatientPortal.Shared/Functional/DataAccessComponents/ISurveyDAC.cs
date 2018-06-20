//-----------------------------------------------------------------------
// <copyright file="ISurveyDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ISurveyDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Survey DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ISurveyDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get list of survey.
        /// </summary>
        /// <returns>List of survey data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<ISurveyDTO> GetSurveyList();

        /// <summary>
        /// Get survey data by survey id.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Survey data component.</returns>
        ISurveyDTO GetSurveyById(int id);

        /// <summary>
        /// Add a survey.
        /// </summary>
        /// <param name="survey">Survey data component.</param>
        /// <returns>True or false.</returns>
        bool InsertSurvey(ISurveyDTO survey);

        /// <summary>
        /// Update a survey.
        /// </summary>
        /// <param name="survey">Survey data component.</param>
        /// <returns>True or false.</returns>
        bool UpdateSurvey(ISurveyDTO survey);

        /// <summary>
        /// Delete a survey.
        /// </summary>
        /// <param name="id">Survey data component.</param>
        /// <returns>True or false.</returns>
        bool DeleteSurveyById(int id);

        IList<ISurveyQuestionMapDTO> GetSurveyQuestionList();

    }
}