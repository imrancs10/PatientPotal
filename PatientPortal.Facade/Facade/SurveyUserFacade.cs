//-----------------------------------------------------------------------
// <copyright file="SurveyUserFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the SurveyUserFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class SurveyUserFacade : FacadeBase, ISurveyUserFacade
    {
        /// <summary>
        /// Instance of SurveyUser BDC.
        /// </summary>
        private readonly ISurveyUserBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyUserFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public SurveyUserFacade(IExceptionManager exceptionManager, ISurveyUserBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
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
                result = this.bdc.GetSurveyQuestionList(surveyId);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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
                result = this.bdc.SaveSurveyAttempt(list);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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
                result = this.bdc.GetSurveyQuestionList(guID);
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