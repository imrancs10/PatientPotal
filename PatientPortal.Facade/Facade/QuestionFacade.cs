//-----------------------------------------------------------------------
// <copyright file="QuestionFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the QuestionFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class QuestionFacade : FacadeBase, IQuestionFacade
    {
        /// <summary>
        /// Instance of Question BDC.
        /// </summary>
        private readonly IQuestionBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public QuestionFacade(IExceptionManager exceptionManager, IQuestionBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<IQuestionDTO>> GetQuestionList()
        {
            OperationResult<IList<IQuestionDTO>> result;
            try
            {
                result = this.bdc.GetQuestionList();
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IQuestionDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IQuestionDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<IQuestionDTO>> GetSurveyQuestionList(int languageId)
        {
            OperationResult<IList<IQuestionDTO>> result;
            try
            {
                result = this.bdc.GetSurveyQuestionList(languageId);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IQuestionDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IQuestionDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
        
        /// <summary>
        /// Gets the question by id.
        /// </summary>
        /// <param name="id">The question id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IQuestionDTO> GetQuestionById(int id)
        {
            OperationResult<IQuestionDTO> result;
            try
            {
                result = this.bdc.GetQuestionById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Insert a question.
        /// </summary>
        /// <param name="questionDTO">Question data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IQuestionDTO> InsertQuestion(IQuestionDTO questionDTO)
        {
            OperationResult<IQuestionDTO> result;
            try
            {
                result = this.bdc.InsertQuestion(questionDTO);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Update a survey.
        /// </summary>
        /// <param name="questionDTO">Question data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IQuestionDTO> UpdateQuestion(IQuestionDTO questionDTO)
        {
            OperationResult<IQuestionDTO> result;
            try
            {
                result = this.bdc.UpdateQuestion(questionDTO);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Delete a question.
        /// </summary>
        /// <param name="id">Question id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IList<IAnswerDTO>> DeleteQuestionById(int id)
        {
            OperationResult<IList<IAnswerDTO>> result;
            try
            {
                result = this.bdc.DeleteQuestionById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IAnswerDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IAnswerDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}