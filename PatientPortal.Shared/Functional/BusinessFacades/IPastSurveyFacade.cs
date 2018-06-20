//-----------------------------------------------------------------------
// <copyright file="IPastSurveyFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IPastSurveyFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Past survey Facade.
    /// </summary>
    public interface IPastSurveyFacade : IFacade
    {
        /// <summary>
        /// Get Past Survey Question Answers list
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns>object of type IList<ISurveyQuestionAnswerDTO></returns>
        OperationResult<IList<ISurveyQuestionAnswerDTO>> GetPastSurveyQuestionList(string guid);

        /// <summary>
        /// This method will use to get the recommended product list.
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        OperationResult<IList<IRecommendedProductDTO>> GetRecommendationList(string Guid);

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        OperationResult<IList<IPastSurveyDTO>> GetPastSurveyList(int userId);

    }
}