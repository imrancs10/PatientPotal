//-----------------------------------------------------------------------
// <copyright file="InputTextTypeFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the InputTextTypeFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for text input type data management module.
    /// </summary>
    public class TextInputTypeFacade : FacadeBase, ITextInputTypeFacade
    {
        /// <summary>
        /// Instance of  text input type BDC.
        /// </summary>
        private readonly ITextInputTypeBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputTypeFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public TextInputTypeFacade(IExceptionManager exceptionManager, ITextInputTypeBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Gets the text input typelist.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<ITextInputTypeDTO>> GetTextInputTypeList()
        {
            OperationResult<IList<ITextInputTypeDTO>> result;
            try
            {
                result = this.bdc.GetTextInputTypeList();
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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