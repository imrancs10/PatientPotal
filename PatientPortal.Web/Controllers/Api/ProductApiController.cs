//-----------------------------------------------------------------------
// <copyright file="ProductApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ProductApiController.cs file.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PatientPortal.Shared;
using PatientPortal.DTOModel;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using System.Web.UI.WebControls;

namespace PatientPortal.Web
{
    [Authorize]
    [RoutePrefix("api/ProductApi")]
    public class ProductApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProductApiController()
        {

        }

        public ProductApiController(IProductFacade facade)
        {
            this.Facade = facade;
        }


        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public ProductApiController(IProductFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IProductFacade Facade { get; set; }

        /// <summary>
        /// This method will use to get the complete product list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<IProductDTO> Get()
        {
            var productList = this.Facade.GetProductList();

            return Utility.GetResultData(productList.Data as IList<IProductDTO>);
        }

        /// <summary>
        /// This method will use to get the particular product by passing its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IProductDTO Get(int id)
        {
            var product = this.Facade.GetProductById(id);
            return Utility.GetResultData(product.Data as IProductDTO);
        }

        /// <summary>
        /// This method will use to post the product.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public OperationResult<IProductDTO> Post(ProductDTO productData)
        {
            IProductDTO iProductDTO = new ProductDTO();
            iProductDTO = AutoMapper.Mapper.Map<IProductDTO, ProductDTO>(productData);
            foreach (ProductImagesDTO productImagesDTO in productData.ProductImagesDTOList)
            {
                iProductDTO.IProductImagesDTOList.Add(productImagesDTO);
            }
            OperationResult<IProductDTO> productDTO = Utility.GetResultData(this.Facade.InsertProduct(iProductDTO));
            if (productDTO.IsValid())
            {
                Logger.LogInfo(CustomLogger.ProductSuccessUpdate(productDTO.Data, PatientPortalConstants.ApplicationKeys.Create));
            }
            else
            {
                Logger.LogInfo(CustomLogger.ProductFailed(PatientPortalConstants.ApplicationKeys.CreateFailed));
            }
            return productDTO;
        }

        /// <summary>
        /// This method will use to update the product.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IProductDTO Put(ProductDTO productData)
        {
            IProductDTO iProductDTO = new ProductDTO();
            iProductDTO = AutoMapper.Mapper.Map<IProductDTO, ProductDTO>(productData);
            foreach (ProductImagesDTO productImagesDTO in productData.ProductImagesDTOList)
            {
                iProductDTO.IProductImagesDTOList.Add(productImagesDTO);
            }
            OperationResult<IProductDTO> productDTOObj = Utility.GetResultData(this.Facade.UpdateProduct(iProductDTO));
            DeleteProductImages(productDTOObj.Data.IProductImagesDTOList);

            if (productDTOObj.IsValid())
            {
                Logger.LogInfo(CustomLogger.ProductSuccessUpdate(productDTOObj.Data, PatientPortalConstants.ApplicationKeys.Update));
            }
            else
            {
                Logger.LogInfo(CustomLogger.ProductFailed(PatientPortalConstants.ApplicationKeys.UpdateFailed));
            }

            return productDTOObj.Data;
        }

        /// <summary>
        /// This method will use to delete the Product.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            OperationResult<IList<IProductImagesDTO>> productImagesList = Utility.GetResultData(Utility.GetResultData(this.Facade.DeleteProductById(id)));
            if (productImagesList != null && productImagesList.Data != null && productImagesList.Data.Count() > 0)
            {
                this.DeleteProductImages(productImagesList.Data);
            }

            return true;
        }

        /// <summary>
        /// Delete images of answers
        /// </summary>
        /// <param name="answerList">List of answers</param>
        [NonAction]
        public void DeleteProductImages(IList<IProductImagesDTO> productImagesList)
        {
            try
            {
                foreach (IProductImagesDTO productImagesDTO in productImagesList)
                {
                    if (!string.IsNullOrEmpty(productImagesDTO.ImagePath))
                    {
                        // Delete main image file
                        System.IO.FileInfo mainFile = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + productImagesDTO.ImagePath));
                        if (mainFile.Exists)
                        {
                            mainFile.Delete();
                        }

                        // Delete thumbnail image file
                        System.IO.FileInfo thumbNailFile = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + productImagesDTO.ThumbnailPath));
                        if (thumbNailFile.Exists)
                        {
                            thumbNailFile.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
        }

        /// <summary>
        /// Save product images
        /// </summary>
        public void SaveProductImage()
        {
            string virtualFolder = string.Empty, physicalFolder = string.Empty, fileName = string.Empty, allFileNames = string.Empty;
            bool isMainImage;
            try
            {
                HttpFileCollection myFileCollection = System.Web.HttpContext.Current.Request.Files;
                if (myFileCollection != null)
                {
                    for (int i = 0; i < myFileCollection.Count; i++)
                    {
                        HttpPostedFile file = myFileCollection[i];
                        isMainImage = Convert.ToInt16(myFileCollection.Keys[i].Substring(0, 1)) % 2 == 1 ? true : false;

                        virtualFolder = isMainImage == true ? PatientPortalConstants.FilePath.ProductImages : PatientPortalConstants.FilePath.ProductThumbnails;
                        physicalFolder = System.Web.HttpContext.Current.Server.MapPath(virtualFolder);
                        fileName = myFileCollection.Keys[i].Substring(1);
                        file.SaveAs(physicalFolder + fileName);
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
