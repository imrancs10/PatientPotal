//-----------------------------------------------------------------------
// <copyright file="IPastSurveyDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IPastSurveyDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for IPastSurvey DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IPastSurveyDAC : IDataAccessComponent
    {
        /// <summary>
        /// This method will use to get the recommended product list.
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        IList<IRecommendedProductDTO> GetRecommendationList(string Guid);

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        IList<IPastSurveyDTO> GetPastSurveyList(int userId);

        /// <summary>
        /// Get Past Survey Question Answers list
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns>object of type IList<ISurveyQuestionAnswerDTO></returns>
        IList<ISurveyQuestionAnswerDTO> GetPastSurveyQuestionList(string Guid);

    }
}