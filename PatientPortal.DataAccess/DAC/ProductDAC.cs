//-----------------------------------------------------------------------
// <copyright file="ProductDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductDAC.cs file.</summary>
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
    public class ProductDAC : DACBase, IProductDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public ProductDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the Product list.
        /// </summary>
        /// <returns>List of Product.</returns>
        public IList<IProductDTO> GetProductList()
        {
            IList<IProductDTO> ProductList = new List<IProductDTO>();

            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<Product> Products = PatientPortalEntities.Products.Include("ProductImages").AsQueryable<Product>();
                if (Products.Count() > 0)
                {
                    foreach (Product product in Products)
                    {
                        IProductDTO productDTO = new ProductDTO();
                        productDTO = Mapper.Map<Product, ProductDTO>(product);
                        productDTO.IsMapped = CheckProductIsMapped(PatientPortalEntities, product);
                        ProductList.Add(productDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching Product list.", ex);
            }

            return ProductList;
        }

        private bool CheckProductIsMapped(PatientPortalEntities PatientPortalEntities, Product product)
        {
            bool isMapped = false;
            SurveyQuestionMap surveyQuestionMap = PatientPortalEntities.SurveyQuestionMaps.Where(c => c.ProductId == product.Id).FirstOrDefault();
            if (surveyQuestionMap != null)
            {
                isMapped = true;
            }
            return isMapped;
        }


        /// <summary>
        /// Gets the Product by id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Product information.</returns>
        public IProductDTO GetProductById(int id)
        {
            IProductDTO productDTO = new ProductDTO();

            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Product product = PatientPortalEntities.Products.Include("ProductImages").AsQueryable<Product>().Where(c => c.Id == id).FirstOrDefault();
                productDTO = Mapper.Map<Product, ProductDTO>(product);
                productDTO.IsMapped = CheckProductIsMapped(PatientPortalEntities, product);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching Product.", ex);
            }

            return productDTO;
        }

        /// <summary>
        /// Add a Product.
        /// </summary>
        /// <param name="Product">Product data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool InsertProduct(IProductDTO productDTO)
        {
            Product ProductEntity = new Product();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                ProductEntity = Mapper.Map<IProductDTO, Product>(productDTO);
                //EntityConverter.FillEntityFromDto(productDTO, ProductEntity);

                PatientPortalEntities.AddToProducts(ProductEntity);
                PatientPortalEntities.SaveChanges();
                if (productDTO.IProductImagesDTOList.Count() > 0)
                {
                    ProductImagesDAC productImagesDAC = new ProductImagesDAC(this.exceptionManager);
                    foreach (IProductImagesDTO iProductImagesDTO in productDTO.IProductImagesDTOList.ToList())
                    {
                        iProductImagesDTO.ProductId = ProductEntity.Id;
                        productImagesDAC.InsertProductImages(iProductImagesDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert Product.", ex);
            }

            return true;
        }

        /// <summary>
        /// Update a Product.
        /// </summary>
        /// <param name="Product">Product data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public IProductDTO UpdateProduct(IProductDTO productDTO)
        {
            try
            {
                List<IProductImagesDTO> productImagesList = new List<IProductImagesDTO>();
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                ProductImagesDAC productImagesDAC = new ProductImagesDAC(this.exceptionManager);
                Product productEntity = PatientPortalEntities.Products.Include("ProductImages").AsQueryable<Product>().Where(c => c.Id == productDTO.Id).FirstOrDefault();

                var newProductImagesList = (from e in productDTO.IProductImagesDTOList
                                            select new
                                            {
                                                productImagesId = e.Id
                                            }).ToList();

                var oldProductImagesList = (from e in productEntity.ProductImages
                                            select new
                                            {
                                                productImagesId = e.Id
                                            }).ToList();

                // Find all the product images to be removed
                var productImagesToBeRemoved = oldProductImagesList.Except(newProductImagesList).ToArray();

                // Find all product images to be updated
                var newProductImagesUpdateList = (from e in productDTO.IProductImagesDTOList
                                            select new
                                            {
                                                productImagesId = e.Id,
                                                isPrimary = e.IsPrimary
                                            }).ToList();

                // product images removed
                if (productImagesToBeRemoved.Count() > 0)
                {
                    foreach (var productImage in productImagesToBeRemoved)
                    {
                        if (productEntity.ProductImages.Any(c => c.Id == productImage.productImagesId))
                        {
                            ProductImage productImageToBeRemove = productEntity.ProductImages.Where(t => t.Id == productImage.productImagesId).FirstOrDefault();
                            PatientPortalEntities.ProductImages.DeleteObject(productImageToBeRemove);
                            var productImageObj = Mapper.Map<ProductImage, ProductImagesDTO>(productImageToBeRemove);
                            productImagesList.Add(productImageObj);
                        }
                    }
                }

                // Add product Images for a product
                if (productDTO.IProductImagesDTOList != null && productDTO.IProductImagesDTOList.Count() > 0)
                {
                    // Save product Images of relevant product
                    foreach (var productImage in productDTO.IProductImagesDTOList)
                    {
                        if (productImage.Id <= 0)
                        {
                            // Insert new record
                            ProductImage productImageEntity = new ProductImage();
                            productImage.ProductId = productEntity.Id;

                            productImageEntity = Mapper.Map<IProductImagesDTO, ProductImage>(productImage);
                            PatientPortalEntities.AddToProductImages(productImageEntity);
                        }
                        else
                        {
                            var updateIsPrimary = productEntity.ProductImages.Where(c => c.Id == productImage.Id).FirstOrDefault();
                            if (updateIsPrimary != null)
                            {
                                updateIsPrimary.IsPrimary = productImage.IsPrimary;
                            }
                        }
                    }
                }


                Mapper.Map<IProductDTO, Product>(productDTO, productEntity);
                
                PatientPortalEntities.SaveChanges();

                productDTO.IProductImagesDTOList.Clear();
                if (productImagesList.Count() > 0)
                {
                    foreach (IProductImagesDTO productImageObj in productImagesList)
                    {
                        productDTO.IProductImagesDTOList.Add(productImageObj);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert Product.", ex);
            }

            return productDTO;
        }

        /// <summary>
        /// Delete a Product.
        /// </summary>
        /// <param name="id">Product data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public List<IProductImagesDTO> DeleteProductById(int id)
        {
            List<IProductImagesDTO> productImagesList = new List<IProductImagesDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Product product = PatientPortalEntities.Products.Include("ProductImages").AsQueryable<Product>().Where(c => c.Id == id).FirstOrDefault();
                if (product.ProductImages.Count() > 0)
                {
                    ProductImagesDAC productImagesDAC = new ProductImagesDAC(this.exceptionManager);
                    foreach (ProductImage productImage in product.ProductImages.ToList())
                    {
                        var productImageObj = Mapper.Map<ProductImage, ProductImagesDTO>(productImage);
                        productImagesList.Add(productImageObj);
                        PatientPortalEntities.Detach(productImage);
                        productImagesDAC.DeleteProductImagesById(productImage.Id);
                    }
                }
                PatientPortalEntities.Products.DeleteObject(product);
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert Product.", ex);
            }

            return productImagesList;
        }
    }
}
