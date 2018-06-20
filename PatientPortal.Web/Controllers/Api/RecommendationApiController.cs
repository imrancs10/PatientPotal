//-----------------------------------------------------------------------
// <copyright file="RecommendationApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the RecommendationApiController.cs file.</summary>
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
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.IO;

namespace PatientPortal.Web
{
    public class RecommendationApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RecommendationApiController()
        {
        }

        /// <summary>
        /// CTOR for initialize IRecommendationFacade
        /// </summary>
        /// <param name="facade">object of type IRecommendationFacade</param>
        public RecommendationApiController(IRecommendationFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public RecommendationApiController(IRecommendationFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IRecommendationFacade Facade { get; set; }

        /// <summary>
        /// Get Recommendation list for the filled survey
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        [HttpGet]
        public IList<IRecommendedProductDTO> GetRecommendationList()
        {
            string Id = string.Empty;
            if (HttpContext.Current.Session["RecommendedGuid"] != null)
            {
                Id = HttpContext.Current.Session["RecommendedGuid"].ToString();
            }
            var recommendationList = this.Facade.GetRecommendationList(Id);
            if (recommendationList.IsValid())
            {
                HttpContext.Current.Session["RecommendedProductList"] = recommendationList.Data;
                Logger.LogInfo(CustomLogger.OnLoadRecommendationSuccess(recommendationList.Data, PatientPortalConstants.ApplicationKeys.Load));
                return recommendationList.Data;
            }
            else
            {
                Logger.LogInfo(CustomLogger.RecommendationFailed(PatientPortalConstants.ApplicationKeys.LoadFailed)); 
                return null;
            }
        }

    }
}
