//-----------------------------------------------------------------------
// <copyright file="IProductImagesDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the IProductImagesDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Product DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IProductImagesDAC : IDataAccessComponent
    {
        /// <summary>
        /// Add a Product.
        /// </summary>
        /// <param name="Product">Product data component.</param>
        /// <returns>True or false.</returns>
        IProductImagesDTO DeleteProductImagesById(int productImagesId);

    }
}
