using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using PatientPortal.DTOModel;
using PatientPortal.Shared;
using System.Linq;
namespace PatientPortal.Web
{
    public static class CustomLogger
    {
        private static StringBuilder logMessage = new StringBuilder();

        #region Product

        /// <summary>
        /// Create product success log
        /// </summary>
        /// <param name="product">Product Object</param>
        /// <returns>Product Log string</returns>
        public static string ProductSuccessUpdate(IProductDTO product, string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(statusType + " Product - " + "SUCCESS ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Product Name: " + (product != null ? product.Name : "") + ", ");
            logMessage.Append("Product URL: " + (product != null ? product.ProductURL : ""));
            logMessage.Append("]");
            return logMessage.ToString();
        }

        private static IUserDetailDTO GetUserDetail()
        {
            if (logMessage.Length > 0)
            {
                logMessage.Remove(0, logMessage.Length);
            }
            IUserDetailDTO userDetailDTO = null;
            if (System.Web.HttpContext.Current.Session["User"] != null)
            {
                userDetailDTO = (System.Web.HttpContext.Current.Session["User"] as PatientPortal.DTOModel.UserDTO).UserDetailDTO;
            }
            return userDetailDTO;
        }

        /// <summary>
        /// Create product failed log
        /// </summary>
        /// <returns>Product Log string</returns>
        public static string ProductFailed(string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(statusType + " Product - " + "FAILED ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Product Name: , ");
            logMessage.Append("Product URL: ");
            logMessage.Append("]");
            return logMessage.ToString();
        }

        #endregion Product

        #region Question

        /// <summary>
        /// Create question success log
        /// </summary>
        /// <param name="question">Question Object</param>
        /// <returns>Question Log string</returns>
        public static string QuestionSuccessUpdate(IQuestionDTO question, string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(statusType + " Question - " + "SUCCESS ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Question: " + (question.Title) + ", ");
            logMessage.Append("IsActive: " + (question.IsActive) + ", ");
            logMessage.Append("IsMandatory: " + (question.IsMandatary));
            logMessage.Append("]");
            return logMessage.ToString();
        }

        /// <summary>
        /// Create Question failed log
        /// </summary>
        /// <returns>Question Log string</returns>
        public static string QuestionFailed(string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(statusType + " Question - " + "FAILED ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Question: , ");
            logMessage.Append("IsActive: , ");
            logMessage.Append("IsMandatory: ");
            logMessage.Append("]");
            return logMessage.ToString();
        }


        #endregion Question

        #region Survey

        /// <summary>
        /// Create survey success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string SurveySuccessUpdate(ISurveyDTO survey, string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(statusType + " Survey - " + "SUCCESS ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Survey: " + (survey.Name) + ", ");
            logMessage.Append("IsActive: " + (survey.IsActive));
            logMessage.Append("]");
            return logMessage.ToString();
        }

        /// <summary>
        /// Create Survey failed log
        /// </summary>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string SurveyFailed(string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(statusType + " Survey - " + "FAILED ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Survey: , ");
            logMessage.Append("IsActive: ");
            logMessage.Append("]");
            return logMessage.ToString();
        }


        #endregion Survey

        #region Survey User

        /// <summary>
        /// Create survey user success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string OnLoadSurveyUserSuccess(ISurveyQuestionMapDTO surveyQuestionAnswerDTO, string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append("Survey Accessed by User  - " + "SUCCESS ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Survey Id: " + (surveyQuestionAnswerDTO.SurveyId) + ", ");
            logMessage.Append("IsActive: " + (surveyQuestionAnswerDTO.ISurveyDTO.IsActive));
            logMessage.Append("]");
            return logMessage.ToString();
        }

        /// <summary>
        /// Create survey user success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string SurveyUserSuccess(ISurveyQuestionAnswerDTO surveyQuestionAnswerDTO, string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append("Submitted Survey by User - " + "SUCCESS ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Survey Id: " + (surveyQuestionAnswerDTO.SurveyId) + ", ");
            logMessage.Append("GUID: " + (surveyQuestionAnswerDTO.Guid) + ", ");
            logMessage.Append("]");
            return logMessage.ToString();
        }

        /// <summary>
        /// Create Survey failed log
        /// </summary>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string SurveyUserFailed(string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append("Survey Accessed by User  - " + "FAILED ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Survey: , ");
            logMessage.Append("IsActive: ");
            logMessage.Append("]");
            return logMessage.ToString();
        }


        #endregion Survey User

        #region Recommendation

        /// <summary>
        /// Create survey user success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string OnLoadRecommendationSuccess(IList<IRecommendedProductDTO> recommendationProductDTOList, string statusType)
        {

            StringBuilder productName = new StringBuilder();
            int productCount = 0;
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append("Recommendation  - " + "SUCCESS ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("GUID: " + (System.Web.HttpContext.Current.Session["RecommendedGuid"].ToString()) + ", ");
            string productList = GetRecommendedProductList(recommendationProductDTOList);
            logMessage.Append("Product Names : [  " + (productList) + " ], ");
            logMessage.Append("]");
            return logMessage.ToString();
        }

        /// <summary>
        /// To get product names with units sum
        /// </summary>
        /// <param name="recommendationProductDTOList">RecommendedProductDTO object</param>
        /// <returns>Product list string</returns>
        private static string GetRecommendedProductList(IList<IRecommendedProductDTO> recommendationProductDTOList)
        {
            StringBuilder strProductList = new StringBuilder();
            List<RecommendedProductDTO> recommendedList = new List<RecommendedProductDTO>();

            System.Collections.IEnumerator sGroup = (from t in recommendationProductDTOList
                                                     group t by t.ProductName into g
                                                     select new
                                                     {
                                                         ProductName = g.Key,
                                                         Units = g.Sum(a => a.RecommendedCount != "" ? (int.Parse(a.RecommendedCount)) : 0)  // Sum, not Max
                                                     }).ToList().GetEnumerator();

            while (sGroup.MoveNext())
            {
                string[] productSplit = sGroup.Current.ToString().Replace("=", "").Replace("ProductName", "").Replace("Units", "").Replace("{", "").Replace("}", "").Trim().Split(',');
                strProductList.Append(productSplit[0] + " Units - " + productSplit[1] == "0" ? "" : productSplit[1] + ", ");
            }
            return strProductList.ToString();
        }

        /// <summary>
        /// Create Survey failed log
        /// </summary>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string RecommendationFailed(string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append("Survey Accessed by User  - " + "FAILED ");
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("Survey: , ");
            logMessage.Append("IsActive: ");
            logMessage.Append("]");
            return logMessage.ToString();
        }


        #endregion Recommendation

        #region Additional Information

        /// <summary>
        /// Create survey user success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string AdditionalInfoSuccess(UserDetailDTO userDetail, string statusType)
        {
            StringBuilder productName = new StringBuilder();
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append("Submitted Additional Information  - " + statusType);
            logMessage.Append(" [");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("GUID: " + (System.Web.HttpContext.Current.Session["RecommendedGuid"].ToString()) + ", ");
            logMessage.Append("Cotnact to Advisor: " + (userDetail.IsAdvisorToContact ? true : false) + ", ");
            logMessage.Append("Download PDF: " + (userDetail.IsPDFDownload ? true : false) + ", ");
            logMessage.Append("Required for Promotion: " + (userDetail.IsReqForPromotions ? true : false));
            logMessage.Append("]");
            return logMessage.ToString();
        }

        /// <summary>
        /// Create survey user success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string RegisterUser(string statusType, bool isPDFDownload, string emailId)
        {
            StringBuilder productName = new StringBuilder();
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(" New User Register -  " + emailId);
            logMessage.Append("[");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("GUID: " + (System.Web.HttpContext.Current.Session["RecommendedGuid"].ToString()) + ", ");
            logMessage.Append("Is PDF Download : " + (isPDFDownload));
            logMessage.Append("]");
            return logMessage.ToString();
        }

        /// <summary>
        /// Create survey user success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string VerifyEmailIdLog(string statusType, bool isAlreadyExisted, string emailId)
        {
            StringBuilder productName = new StringBuilder();
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append(" Verify User Email Id ");
            logMessage.Append(" [");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("Email Id: " + (emailId) + ", ");
            logMessage.Append("GUID: " + (System.Web.HttpContext.Current.Session["RecommendedGuid"].ToString()) + ", ");
            logMessage.Append("EmailId: " + (emailId) + ", ");
            logMessage.Append("User Already Exited : " + (isAlreadyExisted));
            logMessage.Append(" ]");
            return logMessage.ToString();
        }

        #endregion Additional Information

        #region Login

        /// <summary>
        /// Create survey user success log
        /// </summary>
        /// <param name="survey">Survey Object</param>
        /// <param name="statusType">Status Type</param>
        /// <returns>Survey Log string</returns>
        public static string LoginLog(UserDTO userData, string statusType)
        {
            IUserDetailDTO userDetailDTO = GetUserDetail();
            logMessage.Append(Environment.NewLine);
            logMessage.Append(LogLevel(statusType).ToString());
            logMessage.Append("[");
            logMessage.Append("User Login , ");
            logMessage.Append("IP: " + GetIPAddress() + ", ");
            logMessage.Append("User Id: " + (userDetailDTO != null ? userDetailDTO.FirstName : "") + " " + (userDetailDTO != null ? userDetailDTO.LastName : "") + ", ");
            logMessage.Append("User EmailId: " + (userData.UserName));
            logMessage.Append("Successfull : " + (userData.Id > 0 ? true : false));
            logMessage.Append("]");
            return logMessage.ToString();
        }

        #endregion Login

        private static string LogLevel(string statusType)
        {
            StringBuilder stringLogLevel = new StringBuilder();
            string formattedDateTime = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss:fff");

            if (statusType == PatientPortalConstants.ApplicationKeys.Create || statusType == PatientPortalConstants.ApplicationKeys.Update || statusType == PatientPortalConstants.ApplicationKeys.Load || statusType == PatientPortalConstants.ApplicationKeys.Success)
            {
                stringLogLevel.Append("[INFO] ");
            }
            else
            {
                stringLogLevel.Append("[ERROR] ");
            }
            stringLogLevel.Append(formattedDateTime + " :: ");
            return stringLogLevel.ToString();
        }

        /// <summary>
        /// Get IP address
        /// </summary>
        /// <returns>Return IP address as string</returns>
        private static string GetIPAddress()
        {
            string ipAddress = string.Empty;
            ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            return ipAddress;
        }
    }
}