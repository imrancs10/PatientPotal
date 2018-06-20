//-----------------------------------------------------------------------
// <copyright file="ISurveyUserDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ISurveyUserDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for ISurveyUser DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ISurveyUserDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get Question answers list on given survey Id.
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns></returns>
        IList<ISurveyQuestionMapDTO> GetSurveyQuestionList(string surveyId);

        /// <summary>
        /// save attempt survey submitted
        /// </summary>
        /// <param name="list">object of type List<SurveyQuestionAnswerDTO></param>
        /// <returns>object of type ISurveyQuestionAnswerDTO</returns>
        ISurveyQuestionAnswerDTO SaveSurveyAttempt(List<ISurveyQuestionAnswerDTO> list);

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        IList<ISurveyQuestionAnswerDTO> GetSurveyQuestionList(Guid guID);
    }
}