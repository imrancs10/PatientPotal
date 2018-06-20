//-----------------------------------------------------------------------
// <copyright file="IProductBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IProductBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Product Data BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IProductBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Gets the Product list.
        /// </summary>
        /// <returns>List of Product data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IProductDTO>> GetProductList();

        /// <summary>
        /// Gets the Product list.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Product data component.</returns>
        OperationResult<IProductDTO> GetProductById(int id);

        /// <summary>
        /// Insert a Product record.
        /// </summary>
        /// <param name="ProductDTO">Product data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IProductDTO> InsertProduct(IProductDTO ProductDTO);

        /// <summary>
        /// Update a Product record.
        /// </summary>
        /// <param name="ProductDTO">Product data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IProductDTO> UpdateProduct(IProductDTO ProductDTO);

        /// <summary>
        /// Delete a Product record.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IList<IProductImagesDTO>> DeleteProductById(int id);
    }
}
