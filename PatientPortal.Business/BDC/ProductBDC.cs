//-----------------------------------------------------------------------
// <copyright file="ProductBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for Product data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class ProductBDC : BDCBase, IProductBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Product DAC.
        /// </summary>
        private IProductDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public ProductBDC(IExceptionManager exceptionManager, IProductDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Gets the Product list.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<IProductDTO>> GetProductList()
        {
            OperationResult<IList<IProductDTO>> result;
            try
            {
                IList<IProductDTO> ProductList = this.dac.GetProductList();

                if (ProductList == null)
                {
                    result = OperationResult<IList<IProductDTO>>.CreateFailureResult("The object containing Product list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IProductDTO>>.CreateSuccessResult(ProductList, "Product list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IProductDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Gets the Product.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Product data with operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IProductDTO> GetProductById(int id)
        {
            OperationResult<IProductDTO> result;
            try
            {
                IProductDTO ProductDTO = this.dac.GetProductById(id);
                if (ProductDTO == null)
                {
                    result = OperationResult<IProductDTO>.CreateFailureResult("The object containing Product list is NULL !");
                }
                else
                {
                    result = OperationResult<IProductDTO>.CreateSuccessResult(ProductDTO, "Product list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IProductDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Inserts a Product.
        /// </summary>
        /// <param name="ProductDTO">Product data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IProductDTO> InsertProduct(IProductDTO ProductDTO)
        {
            OperationResult<IProductDTO> result;
            try
            {
                ProductDTO.CreatedDate = DateTime.UtcNow;
                ProductDTO.ModifiedDate = DateTime.UtcNow;
                bool ProductResult = this.dac.InsertProduct(ProductDTO);
                if (ProductResult == false)
                {
                    result = OperationResult<IProductDTO>.CreateFailureResult("The Product could not be added !");
                }
                else
                {
                    result = OperationResult<IProductDTO>.CreateSuccessResult(ProductDTO, "Product added successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IProductDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Update a Product.
        /// </summary>
        /// <param name="ProductDTO">Update data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IProductDTO> UpdateProduct(IProductDTO ProductDTO)
        {
            OperationResult<IProductDTO> result;
            try
            {
                ProductDTO.CreatedDate = DateTime.UtcNow;
                ProductDTO.ModifiedDate = DateTime.UtcNow;
                IProductDTO ProductResult = this.dac.UpdateProduct(ProductDTO);
                if (ProductResult == null)
                {
                    result = OperationResult<IProductDTO>.CreateFailureResult("The Product could not be updated !");
                }
                else
                {
                    result = OperationResult<IProductDTO>.CreateSuccessResult(ProductResult, "Product updated successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IProductDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Delete a Product.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<IProductImagesDTO>> DeleteProductById(int id)
        {
            OperationResult<IList<IProductImagesDTO>> result;
            try
            {
                IList<IProductImagesDTO> ProductResult = this.dac.DeleteProductById(id);
                if (ProductResult.Count <= 0)
                {
                    result = OperationResult<IList<IProductImagesDTO>>.CreateFailureResult("The Product could not be updated !");
                }
                else
                {
                    result = OperationResult<IList<IProductImagesDTO>>.CreateSuccessResult(ProductResult, "Product deleted successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IProductImagesDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
