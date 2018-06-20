//-----------------------------------------------------------------------
// <copyright file="IProductImagesBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IProductImagesBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Survey Data BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IProductImagesBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Delete a product image record.
        /// </summary>
        /// <param name="id">product Image id.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IProductImagesDTO> DeleteProductImagesById(int id);
    }
}