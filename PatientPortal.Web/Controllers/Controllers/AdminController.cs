namespace PatientPortal.Web
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using PatientPortal.Business;
    using PatientPortal.DTOModel;
    using PatientPortal.Shared;

    public class AdminController : Controller
    {
        #region Public Member

        /// <summary>
        /// Load view login and pass user DTO
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult Login()
        {
            GetAndSetCulture();
            ViewBag.Script = "Admin/User";
            UserDTO userDTO = new UserDTO();
            return View(userDTO);
        }

        /// <summary>
        /// Load view Survey and pass survey DTO
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        [RoleAuthorizeAttribute(Roles = "Admin", NotifyUrl = "~/login/")]
        public ActionResult Survey()
        {
            GetAndSetCulture();
            ViewBag.Script = "Admin/Survey";
            SurveyDTO surveyDTO = new SurveyDTO();
            return View(surveyDTO);
        }

        /// <summary>
        /// Load view Question and pass Question DTO
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        [RoleAuthorizeAttribute(Roles = "Admin", NotifyUrl = "~/login/")]
        public ActionResult Question()
        {
            GetAndSetCulture();
            ViewBag.Script = "Admin/Question";
            QuestionDTO questionDTO = new QuestionDTO();
            return View(questionDTO);
        }

        /// <summary>
        /// Load view SignOut
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        [RoleAuthorizeAttribute(Roles = "Admin", NotifyUrl = "~/login/")]
        public ActionResult SignOut()
        {
            GetAndSetCulture();
            ViewBag.Script = "Admin/User";
            FormsAuthentication.SignOut();
            Session.Abandon();

            UserDTO userDTO = new UserDTO();
            return View("Login", userDTO);
        }

        /// <summary>
        /// Load view Product
        /// </summary>
        /// <returns>Return a particular View</returns>
        [SessionExpireFilterAttribute]
        [RoleAuthorizeAttribute(Roles = "Admin", NotifyUrl = "~/login/")]
        public ActionResult Product()
        {
            GetAndSetCulture();
            ViewBag.Script = "Admin/Product";
            ProductDTO productDTO = new ProductDTO();
            return View(productDTO);
        }

        /// <summary>
        /// Load view PageNotFound
        /// </summary>
        /// <returns>Return a particular View</returns>
        public ActionResult PageNotFound()
        {
            GetAndSetCulture();
            ViewBag.Script = "Admin/User";
            return View();
        }

        /// <summary>
        /// Get and set cutlture
        /// </summary>
        [NonAction]
        private void GetAndSetCulture()
        {
            string currentCulture = string.Empty;
            int languageId = 0;
            currentCulture =PatientPortalConstants.ConfigurationKeys.defaultLanguage;
            languageId = (int)(EnumLanguage)Enum.Parse(typeof(EnumLanguage), currentCulture.Replace("-", "_"));
            Utility.SetCulture(languageId);
        }
        #endregion
    }
}

