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
        /// Defines a contract for product DTO.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        public interface IProductDTO : IDTO
        {
            /// <summary>
            /// Gets or sets the user id.
            /// </summary>
            /// <value>The user id.</value>
            int Id { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            string Name { get; set; }

            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            string Description { get; set; }

            /// <summary>
            /// Gets or sets the product URL.
            /// </summary>
            /// <value>The product URL.</value>            
            string ProductURL { get; set; }

            /// <summary>
            /// Gets or sets the Language Id.
            /// </summary>
            /// <value>The user id.</value>
            int LanguageId { get; set; }

            /// <summary>
            /// Gets or sets the created date.
            /// </summary>
            /// <value>The createde date.</value>
            DateTime CreatedDate { get; set; }

            /// <summary>
            /// Gets or sets the modified date.
            /// </summary>
            /// <value>The molified date.</value>
            DateTime ModifiedDate { get; set; }

            /// <summary>
            /// Gets or sets the text.
            /// </summary>
            /// <value>The flag for mapping</value>
            bool IsMapped { get; set; }

            IList<IProductImagesDTO> IProductImagesDTOList { get; set; }
        }
    }
