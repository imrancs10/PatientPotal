//-----------------------------------------------------------------------
// <copyright file="ProductImagesBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ProductImagesBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for product images data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class ProductImagesBDC : BDCBase, IProductImagesBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Answer DAC.
        /// </summary>
        private IProductImagesDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImagesBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public ProductImagesBDC(IExceptionManager exceptionManager, IProductImagesDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Delete a product images.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IProductImagesDTO> DeleteProductImagesById(int id)
        {
            OperationResult<IProductImagesDTO> result;
            try
            {
                IProductImagesDTO productImagesResult = this.dac.DeleteProductImagesById(id);
                if (productImagesResult.Id <= 0)
                {
                    result = OperationResult<IProductImagesDTO>.CreateFailureResult("The answer could not be deleted !");
                }
                else
                {
                    result = OperationResult<IProductImagesDTO>.CreateSuccessResult(productImagesResult, "Answer deleted successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IProductImagesDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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