//-----------------------------------------------------------------------
// <copyright file="AnswerFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the AnswerFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class AnswerFacade : FacadeBase, IAnswerFacade
    {
        /// <summary>
        /// Instance of Question BDC.
        /// </summary>
        private readonly IAnswerBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public AnswerFacade(IExceptionManager exceptionManager, IAnswerBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Delete a answer.
        /// </summary>
        /// <param name="id">Answer id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IAnswerDTO> DeleteAnswerById(int id)
        {
            OperationResult<IAnswerDTO> result;
            try
            {
                result = this.bdc.DeleteAnswerById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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