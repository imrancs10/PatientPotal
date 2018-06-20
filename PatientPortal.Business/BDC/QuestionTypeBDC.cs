//-----------------------------------------------------------------------
// <copyright file="QuestionTypeBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the QuestionTypeBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for question type data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class QuestionTypeBDC : BDCBase, IQuestionTypeBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of QuestionType DAC.
        /// </summary>
        private IQuestionTypeDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionTypeBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public QuestionTypeBDC(IExceptionManager exceptionManager, IQuestionTypeDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Gets the question type list.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<IQuestionTypeDTO>> GetQuestionTypeList()
        {
            OperationResult<IList<IQuestionTypeDTO>> result;
            try
            {
                IList<IQuestionTypeDTO> questionTypeList = this.dac.GetQuestionTypeList();

                if (questionTypeList == null)
                {
                    result = OperationResult<IList<IQuestionTypeDTO>>.CreateFailureResult("The object containing question type list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IQuestionTypeDTO>>.CreateSuccessResult(questionTypeList, "Question type list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IQuestionTypeDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IList<IQuestionTypeDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IQuestionTypeDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}