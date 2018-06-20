//-----------------------------------------------------------------------
// <copyright file="QuestionBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the QuestionBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for question data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class QuestionBDC : BDCBase, IQuestionBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Question DAC.
        /// </summary>
        private IQuestionDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public QuestionBDC(IExceptionManager exceptionManager, IQuestionDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<IQuestionDTO>> GetQuestionList()
        {
            OperationResult<IList<IQuestionDTO>> result;
            try
            {
                IList<IQuestionDTO> questionList = this.dac.GetQuestionList();

                if (questionList == null)
                {
                    result = OperationResult<IList<IQuestionDTO>>.CreateFailureResult("The object containing question list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IQuestionDTO>>.CreateSuccessResult(questionList, "Question list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IQuestionDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Gets the question list for survey by language id.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<IQuestionDTO>> GetSurveyQuestionList(int languageId)
        {
            OperationResult<IList<IQuestionDTO>> result;
            try
            {
                IList<IQuestionDTO> questionList = this.dac.GetSurveyQuestionList(languageId);

                if (questionList == null)
                {
                    result = OperationResult<IList<IQuestionDTO>>.CreateFailureResult("The object containing question list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IQuestionDTO>>.CreateSuccessResult(questionList, "Question list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IQuestionDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Gets the question.
        /// </summary>
        /// <param name="id">Question id.</param>
        /// <returns>Question data with operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IQuestionDTO> GetQuestionById(int id)
        {
            OperationResult<IQuestionDTO> result;
            try
            {
                IQuestionDTO questionDTO = this.dac.GetQuestionById(id);
                if (questionDTO == null)
                {
                    result = OperationResult<IQuestionDTO>.CreateFailureResult("The object containing question list is NULL !");
                }
                else
                {
                    result = OperationResult<IQuestionDTO>.CreateSuccessResult(questionDTO, "Survey list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Inserts a question.
        /// </summary>
        /// <param name="questionDTO">Question data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IQuestionDTO> InsertQuestion(IQuestionDTO questionDTO)
        {
            OperationResult<IQuestionDTO> result;
            try
            {
                bool questionResult = this.dac.InsertQuestion(questionDTO);
                if (questionResult == false)
                {
                    result = OperationResult<IQuestionDTO>.CreateFailureResult("The question could not be added !");
                }
                else
                {
                    result = OperationResult<IQuestionDTO>.CreateSuccessResult(questionDTO, "Survey added successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Update a question.
        /// </summary>
        /// <param name="questionDTO">Update data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IQuestionDTO> UpdateQuestion(IQuestionDTO questionDTO)
        {
            OperationResult<IQuestionDTO> result;
            try
            {
                IQuestionDTO questionResult = this.dac.UpdateQuestion(questionDTO);
                if (questionResult == null)
                {
                    result = OperationResult<IQuestionDTO>.CreateFailureResult("The question could not be updated !");
                }
                else
                {
                    result = OperationResult<IQuestionDTO>.CreateSuccessResult(questionResult, "Question updated successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IQuestionDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// <param name="id">Delete data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<IAnswerDTO>> DeleteQuestionById(int id)
        {
            OperationResult<IList<IAnswerDTO>> result;
            try
            {
                IList<IAnswerDTO> questionResult = this.dac.DeleteQuestionById(id);
                if (questionResult.Count <= 0)
                {
                    result = OperationResult<IList<IAnswerDTO>>.CreateFailureResult("The question could not be updated !");
                }
                else
                {
                    result = OperationResult<IList<IAnswerDTO>>.CreateSuccessResult(questionResult, "Question deleted successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IAnswerDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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