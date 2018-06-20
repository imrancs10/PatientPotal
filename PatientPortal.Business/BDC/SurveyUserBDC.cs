//-----------------------------------------------------------------------
// <copyright file="SurveyUserBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the SurveyUserBDC.cs file.</summary>
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
    public class SurveyUserBDC : BDCBase, ISurveyUserBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of SurveyUser DAC.
        /// </summary>
        private ISurveyUserDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyUserBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public SurveyUserBDC(IExceptionManager exceptionManager, ISurveyUserDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Get Question answers list on given survey Id.
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns></returns>
        public OperationResult<IList<ISurveyQuestionMapDTO>> GetSurveyQuestionList(string surveyId)
        {
            OperationResult<IList<ISurveyQuestionMapDTO>> result;
            try
            {
                IList<ISurveyQuestionMapDTO> surveyList = this.dac.GetSurveyQuestionList(surveyId);

                if (surveyList == null)
                {
                    result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateFailureResult("The object containing survey question list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateSuccessResult(surveyList, "Survey question list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// save attempt survey submitted
        /// </summary>
        /// <param name="list">object of type List<SurveyQuestionAnswerDTO></param>
        /// <returns>object of type ISurveyQuestionAnswerDTO</returns>
        public OperationResult<ISurveyQuestionAnswerDTO> SaveSurveyAttempt(List<ISurveyQuestionAnswerDTO> list)
        {
            OperationResult<ISurveyQuestionAnswerDTO> result;
            try
            {

                ISurveyQuestionAnswerDTO surveyResult = this.dac.SaveSurveyAttempt(list);
                if (surveyResult == null)
                {
                    result = OperationResult<ISurveyQuestionAnswerDTO>.CreateFailureResult("The survey attempt could not be added !");
                }
                else
                {
                    result = OperationResult<ISurveyQuestionAnswerDTO>.CreateSuccessResult(surveyResult, "Survey attempt successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<ISurveyQuestionAnswerDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<ISurveyQuestionAnswerDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<ISurveyQuestionAnswerDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        public OperationResult<IList<ISurveyQuestionAnswerDTO>> GetSurveyQuestionList(Guid guID)
        {
            OperationResult<IList<ISurveyQuestionAnswerDTO>> result;
            try
            {
                IList<ISurveyQuestionAnswerDTO> pastSurveyList = this.dac.GetSurveyQuestionList(guID);

                if (pastSurveyList == null)
                {
                    result = OperationResult<IList<ISurveyQuestionAnswerDTO>>.CreateFailureResult("The object containing past survey list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ISurveyQuestionAnswerDTO>>.CreateSuccessResult(pastSurveyList, "Past Survey list fetched successfully!");
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