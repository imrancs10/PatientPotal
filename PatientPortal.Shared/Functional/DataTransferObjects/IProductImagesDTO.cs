//-----------------------------------------------------------------------
// <copyright file="IProductImagesDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary> This is the IProductImagesDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for product DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IProductImagesDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>The product id.</value>
        int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        /// <value>The ImagePath.</value>
        string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the ThumbnailPath.
        /// </summary>
        /// <value>The ThumbnailPath.</value>
        string ThumbnailPath { get; set; }

        /// <summary>
        /// Gets or sets the IsPrimary.
        /// </summary>
        /// <value>The IsPrimary.</value>
        bool IsPrimary { get; set; }
    }
}
