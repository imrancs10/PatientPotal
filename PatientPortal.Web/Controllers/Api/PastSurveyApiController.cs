//-----------------------------------------------------------------------
// <copyright file="PastSurveyApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the PastSurveyApiController.cs file.</summary>
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
    [Authorize]
    public class PastSurveyApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PastSurveyApiController()
        {
        }
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// CTOR for initializeIPastSurveyFacade
        /// </summary>
        /// <param name="facade">object of type IPastSurveyFacade</param>
        public PastSurveyApiController(IPastSurveyFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public PastSurveyApiController(IPastSurveyFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IPastSurveyFacade Facade { get; set; }

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<IPastSurveyDTO> GetPastSurveyList()
        {
            int userId = (HttpContext.Current.Session["User"] as UserDTO).RoleDTO.Type == "SurveyUser" ? (HttpContext.Current.Session["User"] as UserDTO).Id : 0;
            IList<IPastSurveyDTO> pastSurveyListData = null;
            var pastSurveyList = this.Facade.GetPastSurveyList(userId);
            if (pastSurveyList.IsValid())
            {
                foreach (PastSurveyDTO pastSurvey in pastSurveyList.Data)
                {
                    pastSurvey.EncryptSurveyIdLanguageId = this.EncryptSurveyIdLanguageId(pastSurvey.SurveyId, pastSurvey.LanguageId);
                }
             
                pastSurveyListData = pastSurveyList.Data;
            }



            return pastSurveyListData;
        }

        /// <summary>
        /// Get Past Survey Question Answers list
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns>object of type IList<ISurveyQuestionAnswerDTO></returns>
        [HttpGet]
        public IList<ISurveyQuestionAnswerDTO> GetPastSurveyQuestionList(string Id)
        {
            var surveyQuestionList = this.Facade.GetPastSurveyQuestionList(Id);
            if (surveyQuestionList.IsValid())
            {
                return surveyQuestionList.Data;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will use to get the recommended product list.
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        [HttpGet]
        public IList<IRecommendedProductDTO> GetRecommendationListByGuid(string Guid)
        {
            if (!string.IsNullOrEmpty(Guid))
            {
                var surveyQuestionList = this.Facade.GetRecommendationList(Guid);
                if (surveyQuestionList.IsValid())
                {
                    HttpContext.Current.Session["RecommendedProductList"] = surveyQuestionList.Data;
                    return surveyQuestionList.Data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public string EncryptSurveyIdLanguageId(int surveyId, int languageId)
        {
            return (Utility.Encrypt(surveyId + PatientPortalConstants.ApplicationKeys.Backslash + languageId));
        }
    }
}
