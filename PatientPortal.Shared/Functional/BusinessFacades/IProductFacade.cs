//-----------------------------------------------------------------------
// <copyright file="IProductFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the IProductFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for survey Facade.
    /// </summary>
    public interface IProductFacade : IFacade
    {
        /// <summary>
        /// Gets the product list.
        /// </summary>
        /// <returns>List of product.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IProductDTO>> GetProductList();

        /// <summary>
        /// Gets the product by id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Product data.</returns>
        OperationResult<IProductDTO> GetProductById(int id);

        /// <summary>
        /// Insert a product.
        /// </summary>
        /// <param name="survey">Product data.</param>
        OperationResult<IProductDTO> InsertProduct(IProductDTO product);

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="survey">Update data.</param>
        OperationResult<IProductDTO> UpdateProduct(IProductDTO product);

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>True or False with message</returns>
        OperationResult<IList<IProductImagesDTO>> DeleteProductById(int id);
    }
}
