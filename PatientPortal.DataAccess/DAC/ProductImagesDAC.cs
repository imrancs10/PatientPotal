//-----------------------------------------------------------------------
// <copyright file="ProductImagesDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductImagesDAC.cs file.</summary>
//-----------------------------------------------------------------------


namespace PatientPortal.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using PatientPortal.Entities.Entities;
    using PatientPortal.Shared;
    using PatientPortal.DTOModel;
    using PatientPortal.Entities;
    using AutoMapper;

    /// <summary>
    /// Represents Country Data Access Component.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class ProductImagesDAC : DACBase, IProductImagesDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public ProductImagesDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Add a Product.
        /// </summary>
        /// <param name="Product">Product data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool InsertProductImages(IProductImagesDTO iProductImagesDTO)
        {
            ProductImage productImagesEntity = new ProductImage();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                //EntityConverter.FillEntityFromDto(iProductImagesDTO, productImagesEntity);
                productImagesEntity = Mapper.Map<IProductImagesDTO, ProductImage>(iProductImagesDTO);
                PatientPortalEntities.AddToProductImages(productImagesEntity);
                PatientPortalEntities.SaveChanges();
                
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert Product Images.", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a product image from database.
        /// </summary>
        /// <param name="id">Answer id.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public IProductImagesDTO DeleteProductImagesById(int id)
        {
            IProductImagesDTO productImagesDTO = new ProductImagesDTO();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                ProductImage productImage = PatientPortalEntities.ProductImages.AsQueryable<ProductImage>().Where(c => c.Id == id).FirstOrDefault();
                productImagesDTO = Mapper.Map<ProductImage, ProductImagesDTO>(productImage);
                PatientPortalEntities.ProductImages.DeleteObject(productImage);
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while delete product images.", ex);
            }

            return productImagesDTO;
        }

    }
}
