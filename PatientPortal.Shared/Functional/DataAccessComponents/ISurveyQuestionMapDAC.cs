//-----------------------------------------------------------------------
// <copyright file="ISurveyQuestionMapDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ISurveyQuestionMapDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for ISurveyQuestionMapDAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ISurveyQuestionMapDAC : IDataAccessComponent
    {
        /// <summary>
        /// Add a survey.
        /// </summary>
        /// <param name="surveyQuestionMapDTO">Survey Question Map data component.</param>
        /// <returns>Current entity Id.</returns>
        ISurveyQuestionMapDTO InsertSurveyQuestionMap(ISurveyQuestionMapDTO surveyQuestionMapDTO);

       
    }
}