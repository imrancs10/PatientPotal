//-----------------------------------------------------------------------
// <copyright file="RecommendedProductDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the RecommendedProductDTO.cs file.</summary>
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
    /// Represents the DTO class for Recommended Product.
    /// </summary>
    [Serializable]
    [DataContract]
    public class RecommendedProductDTO : DTOBase, IRecommendedProductDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendedProductDTO"/> class.
        /// </summary>
        public RecommendedProductDTO()
        {
            this.ProductImagesDTOList = new List<ProductImagesDTO>();
        }

        /// <summary>
        /// Gets or sets the SurveyId.
        /// </summary>
        /// <value>The Survey Id.</value>
        [DataMember]
        public int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets the SurveyName.
        /// </summary>
        /// <value>The Survey Name.</value>
        [DataMember]
        public string SurveyName { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        /// <value>The Product id.</value>
        [DataMember]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        /// <value>The name of Product.</value>
        [DataMember]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the Product Description.
        /// </summary>
        /// <value>The Product Description.</value>
        [DataMember]
        public string ProductDescription { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the ProductURL.
        /// </summary>
        /// <value>The ProductURL.</value>
        public string ProductURL { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the Product MainImage.
        /// </summary>
        /// <value>The Product MainImage.</value>
        public string ProductMainImage { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the Product LanguageId.
        /// </summary>
        /// <value>The Product LanguageId.</value>
        public int ProductLanguageId { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the Product CreatedDate.
        /// </summary>
        /// <value>The Product CreatedDate.</value>
        public DateTime ProductCreatedDate { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the question text input type.
        /// </summary>
        /// <value>The question text input type.</value>
        public int TextInputType { get; set; }

        /// <summary>
        ///  Gets or sets the IProduct ImagesDTOList.
        /// </summary>
        /// <value>The IProduct ImagesDTOList.</value>
        [DataMember]
        public IList<IProductImagesDTO> IProductImagesDTOList { get; set; }

        /// <summary>
        /// Gets or sets the IProduct ImagesDTOList.
        /// </summary>
        /// <value>The Product ImagesDTOList.</value>
        [DataMember]
        public IList<ProductImagesDTO> ProductImagesDTOList { get; set; }

        /// <summary>
        /// Gets or sets the ErrorCode.
        /// </summary>
        /// <value>The ErrorCode.</value>
        [DataMember]
        public override int ErrorCode { set; get; }

        /// <summary>
        /// Gets or sets the Recommended Count.
        /// </summary>
        /// <value>The RecommendedCount.</value>
        [DataMember]
        public string RecommendedCount { get; set; }

        /// <summary>
        /// Gets or sets the Answer Title.
        /// </summary>
        /// <value>The AnswerTitle.</value>
        [DataMember]
        public string AnswerTitle { get; set; }

        /// <summary>
        /// Gets or sets the DuplicateProductConstraint.
        /// </summary>
        /// <value>The DuplicateProductConstraint.</value>
        [DataMember]
        public string DuplicateProductConstraint { get; set; }
    }
}
