//-----------------------------------------------------------------------
// <copyright file="PastSurveyBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the PastSurveyBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for survey data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class PastSurveyBDC : BDCBase, IPastSurveyBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of IPastSurvey DAC.
        /// </summary>
        private IPastSurveyDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="PastSurveyBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public PastSurveyBDC(IExceptionManager exceptionManager, IPastSurveyDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
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
                IList<IRecommendedProductDTO> surveyList = this.dac.GetRecommendationList(Guid);

                if (surveyList == null)
                {
                    result = OperationResult<IList<IRecommendedProductDTO>>.CreateFailureResult("The object containing survey recommendation list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IRecommendedProductDTO>>.CreateSuccessResult(surveyList, "Survey question list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IRecommendedProductDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
                IList<IPastSurveyDTO> pastSurveyList = this.dac.GetPastSurveyList(userId);

                if (pastSurveyList == null)
                {
                    result = OperationResult<IList<IPastSurveyDTO>>.CreateFailureResult("The object containing past survey list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IPastSurveyDTO>>.CreateSuccessResult(pastSurveyList, "Past Survey list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IPastSurveyDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Get Past Survey Question Answers list
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns>object of type IList<ISurveyQuestionAnswerDTO></returns>
        public OperationResult<IList<ISurveyQuestionAnswerDTO>> GetPastSurveyQuestionList(string Guid)
        {
            OperationResult<IList<ISurveyQuestionAnswerDTO>> result;
            try
            {
                IList<ISurveyQuestionAnswerDTO> surveyList = this.dac.GetPastSurveyQuestionList(Guid);

                if (surveyList == null)
                {
                    result = OperationResult<IList<ISurveyQuestionAnswerDTO>>.CreateFailureResult("The object containing survey question list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ISurveyQuestionAnswerDTO>>.CreateSuccessResult(surveyList, "Survey question list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<ISurveyQuestionAnswerDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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