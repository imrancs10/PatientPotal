//-----------------------------------------------------------------------
// <copyright file="ProductFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductFacade.cs file.</summary>
//-----------------------------------------------------------------------


namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for product data management module.
    /// </summary>
    public class ProductFacade : FacadeBase, IProductFacade
    {
        /// <summary>
        /// Instance of product BDC.
        /// </summary>
        private readonly IProductBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public ProductFacade(IExceptionManager exceptionManager, IProductBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Gets the product list.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<IProductDTO>> GetProductList()
        {
            OperationResult<IList<IProductDTO>> result;
            try
            {
                result = this.bdc.GetProductList();
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IProductDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IProductDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Gets the product by id.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IProductDTO> GetProductById(int id)
        {
            OperationResult<IProductDTO> result;
            try
            {
                result = this.bdc.GetProductById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IProductDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IProductDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Insert a product.
        /// </summary>
        /// <param name="product">Customer data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IProductDTO> InsertProduct(IProductDTO product)
        {
            OperationResult<IProductDTO> result;
            try
            {
                result = this.bdc.InsertProduct(product);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IProductDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IProductDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="product">product data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IProductDTO> UpdateProduct(IProductDTO product)
        {
            OperationResult<IProductDTO> result;
            try
            {
                result = this.bdc.UpdateProduct(product);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IProductDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IProductDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="id">product id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IList<IProductImagesDTO>> DeleteProductById(int id)
        {
            OperationResult<IList<IProductImagesDTO>> result;
            try
            {
                result = this.bdc.DeleteProductById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IProductImagesDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IProductImagesDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}
