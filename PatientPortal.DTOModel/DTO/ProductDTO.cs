//-----------------------------------------------------------------------
// <copyright file="ProductDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductDTO.cs file.</summary>
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
    /// Represents the DTO class for product.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class ProductDTO : DTOBase, IProductDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDTO"/> class.
        /// </summary>
        public ProductDTO()
        {
            this.ProductImagesDTOList = new List<ProductImagesDTO>();
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of customer.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description id.</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ProductURL.
        /// </summary>
        /// <value>The ProductURL.</value>
        [DataMember]
        public string ProductURL { get; set; }

        /// <summary>
        /// Gets or sets the Language Id.
        /// </summary>
        /// <value>The Language Id.</value>
        [DataMember]
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        [DataMember]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The flag for mapping</value>
        [DataMember]
        public bool IsMapped { get; set; }

        /// <summary>
        /// Gets or sets the product images list
        /// </summary>
        /// <value>The list of product images</value>
        [DataMember]
        public IList<IProductImagesDTO> IProductImagesDTOList { get; set; }

        /// <summary>
        /// Gets or sets the product images DTO list
        /// </summary>
        /// <value>The list of product images</value>
        [DataMember]
        public IList<ProductImagesDTO> ProductImagesDTOList { get; set; }

        /// <summary>
        /// Gets or sets the Error Code
        /// </summary>
        /// <value>The Error Code</value>
        public override int ErrorCode { set; get; }
    }
}
