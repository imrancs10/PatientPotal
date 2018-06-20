//-----------------------------------------------------------------------
// <copyright file="ProductImagesFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductImagesFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for productimages data management module.
    /// </summary>
    public class ProductImagesFacade : FacadeBase, IProductImagesFacade
    {
        /// <summary>
        /// Instance of Question BDC.
        /// </summary>
        private readonly IProductImagesBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImagesFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public ProductImagesFacade(IExceptionManager exceptionManager, IProductImagesBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Delete a answer.
        /// </summary>
        /// <param name="id">Answer id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IProductImagesDTO> DeleteProductImagesById(int id)
        {
            OperationResult<IProductImagesDTO> result;
            try
            {
                result = this.bdc.DeleteProductImagesById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IProductImagesDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IProductImagesDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}