//-----------------------------------------------------------------------
// <copyright file="ProductImagesDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductImagesDTO.cs file.</summary>
//-----------------------------------------------------------------------



namespace PatientPortal.DTOModel
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using PatientPortal.Entities;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the DTO class for Product Images.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class ProductImagesDTO : DTOBase, IProductImagesDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDTO"/> class.
        /// </summary>
        public ProductImagesDTO()
        {
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>The product id.</value>
        [DataMember]
        public int ProductId { get; set; }    

        /// <summary>
        /// Gets or sets the ImagePath.
        /// </summary>
        /// <value>The ImagePath.</value>
        [DataMember]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the ThumbnailPath.
        /// </summary>
        /// <value>The ThumbnailPath.</value>
        [DataMember]
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Gets or sets the IsPrimary.
        /// </summary>
        /// <value>The IsPrimary.</value>
        [DataMember]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Gets or sets the Error Code.
        /// </summary>
        /// <value>The ErrorCode.</value>
        [DataMember]
        public override int ErrorCode { set; get; }
    }
}
