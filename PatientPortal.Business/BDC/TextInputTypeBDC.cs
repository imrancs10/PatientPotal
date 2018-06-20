//-----------------------------------------------------------------------
// <copyright file="TextInputTypeBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the TextInputTypeBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for TextInputType data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class TextInputTypeBDC : BDCBase, ITextInputTypeBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of QuestionType DAC.
        /// </summary>
        private ITextInputTypeDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionTypeBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public TextInputTypeBDC(IExceptionManager exceptionManager, ITextInputTypeDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Gets the question type list.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<ITextInputTypeDTO>> GetTextInputTypeList()
        {
            OperationResult<IList<ITextInputTypeDTO>> result;
            try
            {
                IList<ITextInputTypeDTO> textInputTypeList = this.dac.GetTextInputTypeList();

                if (textInputTypeList == null)
                {
                    result = OperationResult<IList<ITextInputTypeDTO>>.CreateFailureResult("The object containing question text input type list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ITextInputTypeDTO>>.CreateSuccessResult(textInputTypeList, "Question  text input type list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<ITextInputTypeDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IList<ITextInputTypeDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ITextInputTypeDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}