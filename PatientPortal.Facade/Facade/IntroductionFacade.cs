//-----------------------------------------------------------------------
// <copyright file="IntroductionFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the IntroductionFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class IntroductionFacade : FacadeBase, IIntroductionFacade
    {
        /// <summary>
        /// Instance of Question BDC.
        /// </summary>
        private readonly IIntroductionBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntroductionFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public IntroductionFacade(IExceptionManager exceptionManager, IIntroductionBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        } 

        /// <summary>
        /// Gets the introduction object.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IIntroductionDTO> GetIntroductionByLanguageId(int id)
        {
            OperationResult<IIntroductionDTO> result;
            try
            {
                result = this.bdc.GetIntroductionByLanguageId(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IIntroductionDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IIntroductionDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
        
       
    }
}