//-----------------------------------------------------------------------
// <copyright file="PastSurveyFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the PastSurveyFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class PastSurveyFacade : FacadeBase, IPastSurveyFacade
    {
        /// <summary>
        /// Instance of survey BDC.
        /// </summary>
        private readonly IPastSurveyBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PastSurveyFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public PastSurveyFacade(IExceptionManager exceptionManager, IPastSurveyBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// This method will use to get the recommended product list.
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        public OperationResult<IList<IRecommendedProductDTO>> GetRecommendationList(string Guid)
        {
            OperationResult<IList<IRecommendedProductDTO>> result;
            try
            {
                result = this.bdc.GetRecommendationList(Guid);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IRecommendedProductDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IRecommendedProductDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        public OperationResult<IList<IPastSurveyDTO>> GetPastSurveyList(int userId)
        {
            OperationResult<IList<IPastSurveyDTO>> result;
            try
            {
                result = this.bdc.GetPastSurveyList(userId);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IPastSurveyDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IPastSurveyDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Get Past Survey Question by GUID
        /// </summary>
        /// <param name="Guid">GUID</param>
        /// <returns>object of type IList<ISurveyQuestionAnswerDTO></returns>
        public OperationResult<IList<ISurveyQuestionAnswerDTO>> GetPastSurveyQuestionList(string Guid)
        {
            OperationResult<IList<ISurveyQuestionAnswerDTO>> result;
            try
            {
                result = this.bdc.GetPastSurveyQuestionList(Guid);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<ISurveyQuestionAnswerDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ISurveyQuestionAnswerDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

    }
}