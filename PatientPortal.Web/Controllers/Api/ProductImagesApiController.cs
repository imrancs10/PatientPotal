//-----------------------------------------------------------------------
// <copyright file="ProductImagesApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the ProductImagesApiController.cs file.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PatientPortal.Shared;
using PatientPortal.DTOModel;

namespace PatientPortal.Web
{
    [Authorize]
    [RoutePrefix("api/ProductImagesApi")]
    public class ProductImagesApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProductImagesApiController()
        {

        }

        public ProductImagesApiController(IProductImagesFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductImagesController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public ProductImagesApiController(IProductImagesFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IProductImagesFacade Facade { get; set; }

        /// <summary>
        /// This method will use to delete the product image.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            OperationResult<IProductImagesDTO> productImages = Utility.GetResultData(Utility.GetResultData(this.Facade.DeleteProductImagesById(id)));
            if (productImages != null && productImages.Data != null)
            {
                this.DeleteProductImages(productImages);
            }
            return true;
        }

        /// <summary>
        /// Delete images of product
        /// </summary>
        /// <param name="answerDTO">Answer Object</param>
        [NonAction]
        public void DeleteProductImages(OperationResult<IProductImagesDTO> productImagesDTO)
        {
            try
            {
                if (!string.IsNullOrEmpty(productImagesDTO.Data.ImagePath))
                {
                    // Delete main image
                    System.IO.FileInfo mainFile = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + productImagesDTO.Data.ImagePath));
                    if (mainFile.Exists)
                    {
                        mainFile.Delete();
                    }

                    // Delete thumbnail image
                    System.IO.FileInfo thumbnailFile = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + productImagesDTO.Data.ThumbnailPath));
                    if (thumbnailFile.Exists)
                    {
                        thumbnailFile.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
        }
    }
}
