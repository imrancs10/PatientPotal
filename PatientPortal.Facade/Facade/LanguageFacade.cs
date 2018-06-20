//-----------------------------------------------------------------------
// <copyright file="LanguageFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the LanguageFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for language data management module.
    /// </summary>
    public class LanguageFacade : FacadeBase, ILanguageFacade
    {
        /// <summary>
        /// Instance of language BDC.
        /// </summary>
        private readonly ILanguageBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public LanguageFacade(IExceptionManager exceptionManager, ILanguageBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<ILanguageDTO>> GetLanguageList()
        {
            OperationResult<IList<ILanguageDTO>> result;
            try
            {
                result = this.bdc.GetLanguageList();
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<ILanguageDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ILanguageDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}