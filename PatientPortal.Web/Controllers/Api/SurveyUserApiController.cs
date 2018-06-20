//-----------------------------------------------------------------------
// <copyright file="SurveyApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the SurveyApiController.cs file.</summary>
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
    public class SurveyUserApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SurveyUserApiController()
        {
        }

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// ctor for initialize ISurveyUserFacade
        /// </summary>
        /// <param name="facade">Facade object</param>
        public SurveyUserApiController(ISurveyUserFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public SurveyUserApiController(ISurveyUserFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private ISurveyUserFacade Facade { get; set; }

        /// <summary>
        /// Get Question answers list on given survey Id.
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns></returns>
        [HttpGet]
        public IList<ISurveyQuestionMapDTO> GetSurveyQuestionList(string Id)
        {
            string pathDownload = System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder);
            //Utility.DeletePDFFiles();

            var surveyQuestionList = this.Facade.GetSurveyQuestionList(Utility.Decrypt(Id));
            if (surveyQuestionList.IsValid())
            {
                Logger.LogInfo(CustomLogger.OnLoadSurveyUserSuccess(surveyQuestionList.Data.FirstOrDefault(), PatientPortalConstants.ApplicationKeys.Load));
                return surveyQuestionList.Data;
            }
            else
            {
                Logger.LogInfo(CustomLogger.SurveyUserFailed(PatientPortalConstants.ApplicationKeys.LoadFailed));
                return null;
            }
        }

        /// <summary>
        /// save attempt survey submitted
        /// </summary>
        /// <param name="list">object of type List<SurveyQuestionAnswerDTO></param>
        /// <returns>object of type ISurveyQuestionAnswerDTO</returns>
        [HttpPost]
        public ISurveyQuestionAnswerDTO SaveSurveyAttempt(List<SurveyQuestionAnswerDTO> surveyQuestionAnswerDTO)
        {
            Guid guid = Guid.NewGuid();
            System.Web.HttpContext.Current.Session["RecommendedGuid"] = guid;
            if (HttpContext.Current.Session["User"] != null && (HttpContext.Current.Session["User"] as UserDTO).RoleDTO.Type == "SurveyUser")
            {
                int userId = (HttpContext.Current.Session["User"] as UserDTO).Id;
                surveyQuestionAnswerDTO.ForEach(x => { x.UserId = userId; });
            }
            surveyQuestionAnswerDTO.ForEach(x => { x.Guid = guid; });
            var result = this.Facade.SaveSurveyAttempt(Mapper.Map<List<SurveyQuestionAnswerDTO>, List<ISurveyQuestionAnswerDTO>>(surveyQuestionAnswerDTO)).Data;
            GetSurveyQuestionList(guid);

            if (result != null && result.Id > 0)
            {
                Logger.LogInfo(CustomLogger.SurveyUserSuccess(result, PatientPortalConstants.ApplicationKeys.Create));
            }
            else
            {
                Logger.LogInfo(CustomLogger.SurveyUserFailed(PatientPortalConstants.ApplicationKeys.CreateFailed));
            }
            return result;
        }

        /// <summary>
        /// Get Past Survey Question Answers list
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns>object of type IList<ISurveyQuestionAnswerDTO></returns>
        public void GetSurveyQuestionList(Guid guId)
        {
            var surveyQuestionList = this.Facade.GetSurveyQuestionList(guId);
            if (surveyQuestionList.IsValid())
            {
                System.Web.HttpContext.Current.Session["CurrentSurveyResult"] = surveyQuestionList.Data;
            }
        }
    }
}
