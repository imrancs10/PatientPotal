//-----------------------------------------------------------------------
// <copyright file="QuestionTypeFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the QuestionTypeFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class QuestionTypeFacade : FacadeBase, IQuestionTypeFacade
    {
        /// <summary>
        /// Instance of question type BDC.
        /// </summary>
        private readonly IQuestionTypeBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionTypeFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public QuestionTypeFacade(IExceptionManager exceptionManager, IQuestionTypeBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Gets the question  typelist.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<IQuestionTypeDTO>> GetQuestionTypeList()
        {
            OperationResult<IList<IQuestionTypeDTO>> result;
            try
            {
                result = this.bdc.GetQuestionTypeList();
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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