//-----------------------------------------------------------------------
// <copyright file="AnswerBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the AnswerBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for answer data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class AnswerBDC : BDCBase, IAnswerBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Answer DAC.
        /// </summary>
        private IAnswerDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public AnswerBDC(IExceptionManager exceptionManager, IAnswerDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Delete a answer.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IAnswerDTO> DeleteAnswerById(int id)
        {
            OperationResult<IAnswerDTO> result;
            try
            {
                IAnswerDTO answerResult = this.dac.DeleteAnswerById(id);
                if (answerResult.Id <= 0)
                {
                    result = OperationResult<IAnswerDTO>.CreateFailureResult("The answer could not be deleted !");
                }
                else
                {
                    result = OperationResult<IAnswerDTO>.CreateSuccessResult(answerResult, "Answer deleted successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IAnswerDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IAnswerDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IAnswerDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}