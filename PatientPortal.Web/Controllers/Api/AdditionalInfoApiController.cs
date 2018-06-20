//-----------------------------------------------------------------------
// <copyright file="AdditionalInfoApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the AdditionalInfoApiController.cs file.</summary>
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
using Microsoft.Owin.Logging;

namespace PatientPortal.Web
{
    public class AdditionalInfoApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AdditionalInfoApiController()
        {
        }

        /// <summary>
        /// CTOR for initialize IAdditionalInfoFacade
        /// </summary>
        /// <param name="facade">object of type IAdditionalInfoFacade</param>
        public AdditionalInfoApiController(IAdditionalInfoFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public AdditionalInfoApiController(IAdditionalInfoFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IAdditionalInfoFacade Facade { get; set; }

        /// <summary>
        /// Get Additional information for a user
        /// </summary>
        /// <returns>object of type IUserDetailDTO</returns>
        [HttpGet]
        public IUserDetailDTO GetAdditionalInformation()
        {
            IUserDetailDTO userDetailDTO = new UserDetailDTO();
            try
            {

                if (HttpContext.Current.Session["User"] != null && (HttpContext.Current.Session["User"] as UserDTO).RoleDTO.Type == "SurveyUser")
                {
                    var surveyQuestionList = this.Facade.GetAdditionalInformation((HttpContext.Current.Session["User"] as UserDTO).Id);
                    if (surveyQuestionList.IsValid())
                    {
                        return surveyQuestionList.Data;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return userDetailDTO;
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
            return userDetailDTO;
        }

        /// <summary>
        /// save additional information
        /// </summary>
        /// <param name="userDetail">object of type UserDetailDTO</param>
        /// <returns>object of type IUserDTO</returns>
        [HttpPost]
        public IUserDTO SaveAdditionalInformation(UserDetailDTO userDetail)
        {
            HttpContext.Current.Session["UserDetail"] = userDetail;
            UserDTO user = new UserDTO();
            try
            {
                if (!string.IsNullOrEmpty(userDetail.Password))
                {
                    userDetail.Password = Utility.Encrypt(userDetail.Password);
                    Logger.LogInfo(CustomLogger.RegisterUser(PatientPortalConstants.ApplicationKeys.Create, userDetail.IsPDFDownload, userDetail.EmailId));
                }
                IUserDetailDTO userdetail = Mapper.Map<UserDetailDTO, IUserDetailDTO>(userDetail);
                user = this.Facade.SaveAdditionalInformation(userdetail).Data as UserDTO;

                if (user != null && user.Id > 0)
                {
                    Logger.LogInfo(CustomLogger.AdditionalInfoSuccess(userDetail, PatientPortalConstants.ApplicationKeys.Success));

                    //Update user ID to survey Anser table
                    if (HttpContext.Current.Session["RecommendedGuid"] != null)
                    {
                        this.Facade.UpdateUserId(HttpContext.Current.Session["RecommendedGuid"].ToString(), user.Id);
                    }
                }
                else
                {
                    Logger.LogInfo(CustomLogger.AdditionalInfoSuccess(userDetail, PatientPortalConstants.ApplicationKeys.Failed));
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
            return user;
        }

        /// <summary>
        /// check user email id 
        /// </summary>
        /// <param name="emailId">object of type IUserDTO</param>
        /// <returns></returns>
        [HttpGet]
        public IUserDTO CheckUserEmail(string emailId)
        {
            var userDTO = this.Facade.CheckUserEmail(emailId);
            if (userDTO.IsValid())
            {
                Logger.LogInfo(CustomLogger.VerifyEmailIdLog(PatientPortalConstants.ApplicationKeys.Create, true, emailId));
                
                return userDTO.Data;
            }
            else
            {
                Logger.LogInfo(CustomLogger.VerifyEmailIdLog(PatientPortalConstants.ApplicationKeys.Create, false, emailId));
                return null;
            }
        }

        /// <summary>
        /// Send mail to security advisor
        /// </summary>
        /// <param name="userDetail">User Detail DTO</param>
        /// <returns>Return a flag whether mail sent or not</returns>
        [HttpPost]
        public bool SendMailAdvisor(UserDetailDTO userDetail)
        {
            try
            {
                string surveyName = string.Empty;
                IList<IRecommendedProductDTO> recommendationList = (IList<IRecommendedProductDTO>)System.Web.HttpContext.Current.Session["RecommendedProductList"];
                if (recommendationList == null)
                {
                    //Exception ex = new NullReferenceException();
                    //Utility.ExplicitlyThrowError(exceptionManager, ex, PatientPortalConstants.Messages.NullRecommendationList);
                }
                else if (recommendationList.Count > 0)
                {
                    //Exception ex = new NullReferenceException();
                    //Utility.ExplicitlyThrowError(exceptionManager, ex, " Recommended list count " + recommendationList.Count);

                    userDetail.SurveyName = recommendationList.FirstOrDefault().SurveyName;
                    surveyName = recommendationList.FirstOrDefault().SurveyName;

                    string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string pathDownload = System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder);

                    string userName = string.Empty, subject = string.Empty, getTemplateString = string.Empty, recommendationAttachedFilePath = string.Empty, ToEmailId = string.Empty;
                    List<string> attachedFiles = new List<string>();
                    recommendationAttachedFilePath = pathDownload + userDetail.PDFFileName;

                    if (userDetail.IsAdvisorToContact)
                    {
                        List<string> ccEmailId;
                        AdvisorMailContent(userDetail, pathDownload, ref subject, ref getTemplateString, ref recommendationAttachedFilePath, attachedFiles, out ToEmailId, out ccEmailId);
                        Utility.SendMail(subject, getTemplateString, ToEmailId, ccEmailId, attachedFiles, this.exceptionManager);
                        //DeletePDFFile((PatientPortalConstants.FilePath.PDFFileFolder + userDetail.PDFFileName));
                    }
                   

                    List<string> ccAddress = new List<string>();
                    attachedFiles = new List<string>();
                    ToEmailId = string.Empty; subject = string.Empty;
                    string surveyResultEmailTemplate = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.SurveyResultBodyTemplate));

                    subject = surveyName + PatientPortalConstants.ApplicationKeys.ResultsFor + userDetail.FirstName + PatientPortalConstants.ApplicationKeys.SingleSpace + userDetail.LastName;

                    getTemplateString = surveyResultEmailTemplate.Replace("%Name%", userDetail.FirstName + PatientPortalConstants.ApplicationKeys.SingleSpace + userDetail.LastName).Replace("%BusinessName%", userDetail.BusinessName).Replace("%PhoneNumber%", userDetail.PhoneNumber).Replace("%ZipCode%", userDetail.ZipCode).Replace("%EmailId%", userDetail.EmailId).Replace("%StateName%", userDetail.StateName.ToString());
                    ToEmailId = PatientPortalConstants.ConfigurationKeys.surveyResultTo;

                    string surveyResultFile = recommendationAttachedFilePath.Replace(PatientPortalConstants.FileName.RecommendedProduct, (surveyName + PatientPortalConstants.ApplicationKeys.Underscore + userDetail.LastName + PatientPortalConstants.ApplicationKeys.Underscore + userDetail.FirstName + PatientPortalConstants.FileName.SurveyResult));

                    attachedFiles.Add(surveyResultFile);
                    Logger.LogInfo(CustomLogger.AdditionalInfoSuccess(userDetail, PatientPortalConstants.ApplicationKeys.Create));
                    Utility.SendMail(subject, getTemplateString, ToEmailId, ccAddress, attachedFiles, this.exceptionManager);
                    //DeletePDFFile(surveyResultFile);
                }
                else
                {
                    Logger.LogInfo(CustomLogger.AdditionalInfoSuccess(userDetail, PatientPortalConstants.ApplicationKeys.Create));
                    //Exception ex = new NullReferenceException();
                    //Utility.ExplicitlyThrowError(exceptionManager, ex, " Recommended list count " + recommendationList.Count);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
            return true;
        }

        /// <summary>
        /// Advisor mail content
        /// </summary>
        /// <param name="userDetail">user detail DTO</param>
        /// <param name="pathDownload">pdf file complete path</param>
        /// <param name="subject">subject for the mail</param>
        /// <param name="getTemplateString">Template body</param>
        /// <param name="recommendationAttachedFilePath">pdf file complete path</param>
        /// <param name="attachedFiles">File attached in Email</param>
        /// <param name="ToEmailId">To email address</param>
        /// <param name="ccEmailId">CC email address</param>
        private static void AdvisorMailContent(UserDetailDTO userDetail, string pathDownload, ref string subject, ref string getTemplateString, ref string recommendationAttachedFilePath, List<string> attachedFiles, out string ToEmailId, out List<string> ccEmailId)
        {
            string advisorEmailTemplate = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.AdvisorEmailTemplate));
            subject = PatientPortalConstants.ConfigurationKeys.securityAdvisorMailSubject;
            getTemplateString = advisorEmailTemplate.Replace("%Name%", userDetail.FirstName + PatientPortalConstants.ApplicationKeys.SingleSpace + userDetail.LastName).Replace("%BusinessName%", userDetail.BusinessName).Replace("%PhoneNumber%", userDetail.PhoneNumber).Replace("%ZipCode%", userDetail.ZipCode).Replace("%EmailId%", userDetail.EmailId).Replace("%StateName%", userDetail.StateName.ToString());
            ToEmailId = Convert.ToString(userDetail.TerritoryEmails.First());
            ccEmailId = userDetail.TerritoryEmails.Skip(1).ToList();
            recommendationAttachedFilePath = pathDownload + userDetail.PDFFileName;
            attachedFiles.Add(recommendationAttachedFilePath);
        }

        /// <summary>
        /// Delete PDF files
        /// </summary>
        /// <param name="filePath">PDF file path</param>
        [NonAction]
        public void DeletePDFFile(string folderPath)
        {
            string getDay = System.DateTime.Now.AddDays(-1).ToString(); //.Month.ToString() +"-"+ System.DateTime.Now.Day.ToString() + "-"+

            DirectoryInfo directory = new DirectoryInfo(folderPath);

            foreach (var file in directory.GetFiles("*.pdf"))
            {
                
            }


            //if (!string.IsNullOrEmpty(filePath))
            //{
            //    System.IO.FileInfo file = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath(filePath));
            //    if (file.Exists)
            //    {
            //        file.Delete();
            //    }
            //}
        }
    }
}
