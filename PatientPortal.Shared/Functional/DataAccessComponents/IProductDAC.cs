//-----------------------------------------------------------------------
// <copyright file="IProductDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the IProductDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Product DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IProductDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get list of Product.
        /// </summary>
        /// <returns>List of Product data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<IProductDTO> GetProductList();

        /// <summary>
        /// Get Product data by Product id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Product data component.</returns>
        IProductDTO GetProductById(int id);

        /// <summary>
        /// Add a Product.
        /// </summary>
        /// <param name="Product">Product data component.</param>
        /// <returns>True or false.</returns>
        bool InsertProduct(IProductDTO Product);

        /// <summary>
        /// Update a Product.
        /// </summary>
        /// <param name="Product">Product data component.</param>
        /// <returns>Product DTO.</returns>
        IProductDTO UpdateProduct(IProductDTO Product);

        /// <summary>
        /// Delete a Product.
        /// </summary>
        /// <param name="id">Product data component.</param>
        /// <returns>True or false.</returns>
        List<IProductImagesDTO> DeleteProductById(int id);

    }
}
