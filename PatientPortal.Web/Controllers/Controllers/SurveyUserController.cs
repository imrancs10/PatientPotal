
namespace PatientPortal.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Security;
    using PatientPortal.Business;
    using PatientPortal.DTOModel;
    using PatientPortal.Shared;
    using PatientPortal.Plugin.Log4net;

    public class SurveyUserController : Controller
    {
        #region Public Member

        /// <summary>
        /// Load view Index and pass survey DTO
        /// </summary>
        /// <param name="surveyId">surveyId</param>
        /// <returns>Return a particular View</returns>
        public ActionResult Index(string surveyId)
        {
            SurveyDTO survetDTO = new SurveyDTO();
            if (System.Web.HttpContext.Current.Response.Cookies.Get("Language") != null)
            {
                System.Web.HttpContext.Current.Request.Cookies["Language"].Expires = DateTime.Now.AddDays(-1);
            }
            int languageId = Convert.ToInt16(Utility.Decrypt(surveyId).Split('/')[1]);
            Utility.SetCulture(languageId);
            ViewBag.Script = "User/Survey";
            survetDTO.EncryptSurveyIdLanguageId = surveyId;

            return View(survetDTO);
        }

        /// <summary>
        /// Load view Recommendation
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        public ActionResult Recommendation()
        {
            GetAndSetCulture();
            ViewBag.Script = "User/Recommendation";
            return View();
        }

        /// <summary>
        /// Load view RecommendationPage
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        public ActionResult RecommendationPage()
        {
            GetAndSetCulture();
            ViewBag.Script = "User/Recommendation";
            return View("Recommendation");
        }

        /// <summary>
        /// Load view PastSurvey
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        [RoleAuthorizeAttribute(Roles = "SurveyUser", NotifyUrl = "~/login/")]
        public ActionResult PastSurvey()
        {
            GetAndSetCulture();
            ViewBag.Script = "User/PastSurvey";
            return View();
        }

        /// <summary>
        /// Load view AdditionalInformation
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        public ActionResult AdditionalInformation()
        {
            GetAndSetCulture();
            ViewBag.Script = "User/AdditionalInformation";
            return View();
        }

        /// <summary>
        /// To get confirm password request of a user
        /// </summary>
        /// <param name="emailId">parameter as emailId</param>
        /// <returns>Return a view</returns>
        public ActionResult ConfirmPassword(string emailId)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserName = emailId;
            ViewBag.Script = "Admin/User";
            return View(userDTO);
        }

        /// <summary>
        /// Create PDF on ViewAsPdf action
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult CreatePDF()
        {
            IList<RecommendedProductDTO> recommendationListObj = new List<RecommendedProductDTO>();
            if (System.Web.HttpContext.Current.Session["RecommendedProductList"] != null)
            {
                List<IRecommendedProductDTO> recommendationList = System.Web.HttpContext.Current.Session["RecommendedProductList"] as List<IRecommendedProductDTO>;
                if (recommendationList.Count > 0)
                {
                    foreach (IRecommendedProductDTO abc in recommendationList)
                    {
                        RecommendedProductDTO obj = AutoMapper.Mapper.Map<IRecommendedProductDTO, RecommendedProductDTO>(abc);
                        recommendationListObj.Add(obj);
                    }
                }
            }
            return View(recommendationListObj);
        }

        /// <summary>
        /// Create PDF on ViewAsPdf action
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult PdfFooter()
        {
            return View();
        }

        /// <summary>
        /// Download PDF on with appropriate data
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult DownloadPDF(string file)
        {
            string errorCode = string.Empty;
            try
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                string surveyName = string.Empty;
                string pathDownload = System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder);
                IList<RecommendedProductDTO> recommendationListObj = new List<RecommendedProductDTO>();

                if (System.Web.HttpContext.Current.Session["RecommendedProductList"] != null)
                {
                    IList<IRecommendedProductDTO> recommendationList = (IList<IRecommendedProductDTO>)System.Web.HttpContext.Current.Session["RecommendedProductList"];
                    if (recommendationList.Count > 0)
                    {
                        surveyName = recommendationList.FirstOrDefault().SurveyName;
                        foreach (IRecommendedProductDTO recommendedProduct in recommendationList)
                        {
                            RecommendedProductDTO obj = AutoMapper.Mapper.Map<IRecommendedProductDTO, RecommendedProductDTO>(recommendedProduct);
                            recommendationListObj.Add(obj);
                        }
                    }
                }
                else
                {
                    errorCode = "1"; // PDF file not able to download
                }

                // Download survey Result
                DownloadSurveyResult(file, surveyName);

                var fileContent = new Rotativa.ViewAsPdf("CreatePDF", recommendationListObj)
                {
                    FileName = file,
                    PageSize = Rotativa.Options.Size.A4,
                    PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
                };
                byte[] byteArray = fileContent.BuildPdf(ControllerContext);

                var fileStream = new FileStream((pathDownload + file), FileMode.Create, FileAccess.Write);
                fileStream.Write(byteArray, 0, byteArray.Length);
                fileStream.Close();
            
            }
            catch (Exception ex)
            {

            }
          
            return Content(errorCode);
        }

        public void DownloadSurveyResult(string file, string surveyName)
        {
            try
            {
                string pathUser = string.Empty, pathDownload = string.Empty, fileName = string.Empty;
                pathDownload = System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder);
                fileName = file;
                UserDetailDTO userDetails = (UserDetailDTO)System.Web.HttpContext.Current.Session["UserDetail"];
                fileName = fileName.Replace(PatientPortalConstants.FileName.RecommendedProduct, (surveyName + PatientPortalConstants.ApplicationKeys.Underscore + userDetails.LastName + PatientPortalConstants.ApplicationKeys.Underscore + userDetails.FirstName + PatientPortalConstants.FileName.SurveyResult));

                IList<SurveyQuestionAnswerDTO> currentSurveyDTOList = new List<SurveyQuestionAnswerDTO>();
                if (System.Web.HttpContext.Current.Session["CurrentSurveyResult"] != null)
                {
                    IList<ISurveyQuestionAnswerDTO> currentSurveyList = (IList<ISurveyQuestionAnswerDTO>)System.Web.HttpContext.Current.Session["CurrentSurveyResult"];
                    if (currentSurveyList.Count > 0)
                    {
                        foreach (ISurveyQuestionAnswerDTO currentSurvey in currentSurveyList)
                        {
                            SurveyQuestionAnswerDTO objCurrentSurvey = AutoMapper.Mapper.Map<ISurveyQuestionAnswerDTO, SurveyQuestionAnswerDTO>(currentSurvey);
                            currentSurveyDTOList.Add(objCurrentSurvey);
                        }
                    }
                }

                var fileContent = new Rotativa.ViewAsPdf("SurveyResult", currentSurveyDTOList)
                {
                    FileName = fileName,
                    PageSize = Rotativa.Options.Size.A4,
                    PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
                };

                byte[] byteArray = fileContent.BuildPdf(ControllerContext);

                var fileStream = new FileStream((pathDownload + fileName), FileMode.Create, FileAccess.Write);
                fileStream.Write(byteArray, 0, byteArray.Length);
                fileStream.Close();
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// Download PDF on with appropriate data
        /// </summary>
        /// <returns>Return a particular View</returns>
        public void DownloadUserSurveyResult(string file, string surveyName)
        {
            try
            {
                // Download survey Result
                DownloadSurveyResult(file, surveyName);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Download PDF on with appropriate data
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult PDFAttached(string file)
        {
            string errorCode = string.Empty;
            try
            {
                string pathDownload = System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder);
                string fileName = string.Empty, surveyName = string.Empty;
                DateTime dateTime = DateTime.UtcNow.Date;
                fileName = file;
                IList<RecommendedProductDTO> recommendationListObj = new List<RecommendedProductDTO>();
                if (System.Web.HttpContext.Current.Session["RecommendedProductList"] != null)
                {
                    IList<IRecommendedProductDTO> recommendationList = (IList<IRecommendedProductDTO>)System.Web.HttpContext.Current.Session["RecommendedProductList"];
                    if (recommendationList.Count > 0)
                    {
                        surveyName = recommendationList.FirstOrDefault().SurveyName;
                        foreach (IRecommendedProductDTO recommendedProduct in recommendationList)
                        {
                            RecommendedProductDTO obj = AutoMapper.Mapper.Map<IRecommendedProductDTO, RecommendedProductDTO>(recommendedProduct);
                            recommendationListObj.Add(obj);
                        }
                    }
                }
                else
                {
                    errorCode = "2"; // PDF file generated for attached
                }

                var fileContent = new Rotativa.ViewAsPdf("CreatePDF", recommendationListObj)
                    {
                        FileName = fileName,
                        PageSize = Rotativa.Options.Size.A4,
                        PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
                    };


                byte[] byteArray = fileContent.BuildPdf(ControllerContext);

                var fileStream = new FileStream((pathDownload + fileName), FileMode.Create, FileAccess.Write);
                fileStream.Write(byteArray, 0, byteArray.Length);
                fileStream.Close();

               // Download survey Result
               DownloadSurveyResult(file, surveyName);
            }
            catch (Exception ex)
            {

            }
            return Content(errorCode);
        }

        /// <summary>
        /// Download PDF on with appropriate data
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult DownloadPDFAndAttachedPDF(string file)
        {
            string errorCode = string.Empty, result = string.Empty;
            try
            {
                string surveyName = string.Empty;
                string pathDownload = System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder);
                DateTime dateTime = DateTime.UtcNow.Date;
                IList<RecommendedProductDTO> recommendationListObj = new List<RecommendedProductDTO>();
                if (System.Web.HttpContext.Current.Session["RecommendedProductList"] != null)
                {
                    IList<IRecommendedProductDTO> recommendationList = (IList<IRecommendedProductDTO>)System.Web.HttpContext.Current.Session["RecommendedProductList"];
                    if (recommendationList.Count > 0)
                    {
                        surveyName = recommendationList.FirstOrDefault().SurveyName;
                        foreach (IRecommendedProductDTO recommendedProduct in recommendationList)
                        {
                            RecommendedProductDTO obj = AutoMapper.Mapper.Map<IRecommendedProductDTO, RecommendedProductDTO>(recommendedProduct);
                            recommendationListObj.Add(obj);
                        }
                    }
                }
                else
                {
                    errorCode = "3"; // PDF file not able to download
                }

                var fileContent = new Rotativa.ViewAsPdf("CreatePDF", recommendationListObj)
                    {
                        FileName = file,
                        PageSize = Rotativa.Options.Size.A4,
                        PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
                    };


                byte[] byteArray = fileContent.BuildPdf(ControllerContext);

                var fileStream = new FileStream((pathDownload + file), FileMode.Create, FileAccess.Write);
                fileStream.Write(byteArray, 0, byteArray.Length);
                fileStream.Close();

                // Download survey Result
                DownloadSurveyResult(file, surveyName);
            }
            catch (Exception ex)
            {

            }
            return new EmptyResult();
        }

        /// <summary>
        /// Download PDF on with appropriate data
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult DownloadPDFPastSurvey()
        {
            string fileName = string.Empty;
            DateTime dateTime = DateTime.UtcNow.Date;
            fileName = "Yale Multi-Family Housing Product Recommendation-" + dateTime.ToString("MM-dd-yyyy") + PatientPortalConstants.ApplicationKeys.Hypen + DateTime.UtcNow.Minute + PatientPortalConstants.ApplicationKeys.Hypen + DateTime.UtcNow.Second + ".pdf";
            IList<RecommendedProductDTO> recommendationListObj = new List<RecommendedProductDTO>();
            if (System.Web.HttpContext.Current.Session["RecommendedProductList"] != null)
            {
                GetAndSetCulture();
                IList<IRecommendedProductDTO> recommendationList = (IList<IRecommendedProductDTO>)System.Web.HttpContext.Current.Session["RecommendedProductList"];
                if (recommendationList.Count > 0)
                {
                    foreach (IRecommendedProductDTO recommendedProduct in recommendationList)
                    {
                        RecommendedProductDTO obj = AutoMapper.Mapper.Map<IRecommendedProductDTO, RecommendedProductDTO>(recommendedProduct);
                        recommendationListObj.Add(obj);
                    }
                }
            }

            //return View("CreatePDF", recommendationListObj);

            return new Rotativa.ViewAsPdf("CreatePDF", recommendationListObj)
            {
                FileName = fileName,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
            };
        }

        /// <summary>
        /// Get and set cutlture
        /// </summary>
        [NonAction]
        private void GetAndSetCulture()
        {
            string currentCulture = string.Empty;
            int languageId = 0;
            if (System.Web.HttpContext.Current.Response.Cookies.Get("Language") == null)
            {
                currentCulture = PatientPortalConstants.ConfigurationKeys.defaultLanguage;
                languageId = (int)(EnumLanguage)Enum.Parse(typeof(EnumLanguage), currentCulture.Replace(PatientPortalConstants.ApplicationKeys.Hypen, PatientPortalConstants.ApplicationKeys.Underscore));
            }
            else
            {
                currentCulture = System.Web.HttpContext.Current.Request.Cookies["Language"].Value.ToString();
                languageId = (int)(EnumLanguage)Enum.Parse(typeof(EnumLanguage), currentCulture.Replace(PatientPortalConstants.ApplicationKeys.Hypen, PatientPortalConstants.ApplicationKeys.Underscore));
            }
            Utility.SetCulture(languageId);
        }

        #endregion

    }
}

