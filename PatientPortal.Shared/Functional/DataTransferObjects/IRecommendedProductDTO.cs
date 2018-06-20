    //-----------------------------------------------------------------------
    // <copyright file="IProductDTO.cs" company="Company" author="Nagarro">
    //  All rights reserved. Copyright (c) 2016.
    // </copyright>
    // <summary> This is the IProductDTO class.</summary>
    //-----------------------------------------------------------------------

    namespace PatientPortal.Shared
    {
        using System;
        using System.Collections.Generic;
        using System.Diagnostics.CodeAnalysis;

        /// <summary>
        /// Defines a contract for Recommended Product DTO.
        /// </summary>
        public interface IRecommendedProductDTO : IDTO
        {
            /// <summary>
            /// Gets or sets the SurveyId.
            /// </summary>
            /// <value>The Survey Id.</value>
            int SurveyId { get; set; }

            /// <summary>
            /// Gets or sets the SurveyName.
            /// </summary>
            /// <value>The Survey Name.</value>
            string SurveyName { get; set; }

            /// <summary>
            /// Gets or sets the ProductId.
            /// </summary>
            /// <value>The Product id.</value>
            int ProductId { get; set; }

            /// <summary>
            /// Gets or sets the ProductName.
            /// </summary>
            /// <value>The name of Product.</value>
            string ProductName { get; set; }

            /// <summary>
            /// Gets or sets the Product Description.
            /// </summary>
            /// <value>The Product Description.</value>
            string ProductDescription { get; set; }

            /// <summary>
            /// Gets or sets the ProductURL.
            /// </summary>
            /// <value>The ProductURL.</value>     
            string ProductURL { get; set; }

            /// <summary>
            /// Gets or sets the Product MainImage.
            /// </summary>
            /// <value>The Product MainImage.</value>
            string ProductMainImage { get; set; }

            /// <summary>
            /// Gets or sets the Product LanguageId.
            /// </summary>
            /// <value>The Product LanguageId.</value>
            int ProductLanguageId { get; set; }

            /// <summary>
            /// Gets or sets the Product CreatedDate.
            /// </summary>
            /// <value>The Product CreatedDate.</value>
            DateTime ProductCreatedDate { get; set; }

            /// <summary>
            /// Gets or sets the IProductImagesDTOList.
            /// </summary>
            /// <value>The IProductImagesDTOList.</value>
            IList<IProductImagesDTO> IProductImagesDTOList { get; set; }

            /// <summary>
            /// Gets or sets the Recommended Count.
            /// </summary>
            /// <value>The RecommendedCount.</value>
            string RecommendedCount { get; set; }

            /// <summary>
            /// Gets or sets the question text input type.
            /// </summary>
            /// <value>The question text input type.</value>
            int TextInputType { get; set; }

            /// <summary>
            /// Gets or sets the Answer Title.
            /// </summary>
            /// <value>The AnswerTitle.</value>
            string AnswerTitle { get; set; }

            /// <summary>
            /// Gets or sets the DuplicateProductConstraint.
            /// </summary>
            /// <value>The DuplicateProductConstraint.</value>
            string DuplicateProductConstraint { get; set; }
        }
    }
