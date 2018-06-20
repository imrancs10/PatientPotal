//-----------------------------------------------------------------------
// <copyright file="UserApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the UserApiController.cs file.</summary>
//-----------------------------------------------------------------------
using System.Web.Http;
using PatientPortal.Shared;
using PatientPortal.DTOModel;
using System.Web.Security;
using System.Collections.Generic;
using System.Web;
using System;

namespace PatientPortal.Web
{
    [RoutePrefix("api/UserApi")]
    public class UserApiController : BaseApiController, System.Web.SessionState.IRequiresSessionState
    {
        #region Private Member
        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IUserFacade Facade { get; set; }

        #endregion

        #region Constructor
        public UserApiController()
        {

        }

        public UserApiController(IUserFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public UserApiController(IUserFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }
        #endregion

        #region Public Member
        /// <summary>
        /// This method will use to get the complete user list.
        /// </summary>
        /// <returns>UserDTO object</returns>
        [HttpPost]
        public IUserDTO Post(UserDTO userData)
        {
            OperationResult<IUserDTO> userDTO = null;
            UserDTO user = null;
            if (!userData.IsForgetPassword)
            {
                user = this.AuthenticateUser(userData, userDTO, user);
                if (user != null && user.Id > 0)
                {
                    Logger.LogInfo(CustomLogger.LoginLog(userData, PatientPortalConstants.ApplicationKeys.Success));
                }
                else
                {
                    Logger.LogInfo(CustomLogger.LoginLog(userData, PatientPortalConstants.ApplicationKeys.Failed));
                }
            }
            else
            {
                user = this.UserForgetPasswordSendMail(userData, userDTO, user);
            }
            return user;
        }

        /// <summary>
        /// This method will use to update the user.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public OperationResult<IUserDTO> Put(UserDTO userDTO)
        {
            userDTO.UserName = Utility.Decrypt(userDTO.UserName);
            userDTO.Password = Utility.Encrypt(userDTO.Password);
            return Utility.GetResultData(this.Facade.UpdateUser(userDTO));
        }

        /// <summary>
        /// Get user information and send mail to the user
        /// </summary>
        /// <param name="userData">User data</param>
        /// <param name="userDTO">Get information of user from DB</param>
        /// <param name="user">Convert it into entity</param>
        /// <returns>Return a user DTO</returns>
        [NonAction]
        private UserDTO AuthenticateUser(UserDTO userData, OperationResult<IUserDTO> userDTO, UserDTO user)
        {
            try
            {
                userData.Password = Utility.Encrypt(userData.Password);
                userDTO = this.Facade.CheckAuthenticUser(userData);

                user = Utility.GetResultData(userDTO.Data as UserDTO);
                user.ErrorCode = Utility.SetMessage(userDTO);
                if (user != null && user.Id > 0)
                {
                    PatientPortalRole PatientPortalRole = new PatientPortalRole();

                    // Create role to RoleProvider
                    PatientPortalRole.CreateRole(user.RoleDTO.Type);

                    // Set authentication cookies
                    FormsAuthentication.SetAuthCookie(user.UserName, false);

                    Utility.SetUserSession(user);

                    //Update user ID to survey Anser table
                    if (HttpContext.Current.Session["RecommendedGuid"] != null)
                    {
                        this.Facade.UpdateUserId(HttpContext.Current.Session["RecommendedGuid"].ToString(), user.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
            return user;
        }

        /// <summary>
        /// Get user information from their user name and password and authenticate user
        /// </summary>
        /// <param name="userData">User data</param>
        /// <param name="userDTO">Get information of user from DB</param>
        /// <param name="user">Convert it into entity</param>
        /// <returns>Return a user DTO</returns>
        [NonAction]
        private UserDTO UserForgetPasswordSendMail(UserDTO userData, OperationResult<IUserDTO> userDTO, UserDTO user)
        {
            try
            {
                string userName = string.Empty, subject = string.Empty, forgetPasswordLink = string.Empty, getTemplateString = string.Empty;
                List<string> attachedFiles = new List<string>();
                userDTO = this.Facade.ForgetPassword(userData);
                user = Utility.GetResultData(userDTO.Data as UserDTO);
                if (user != null && user.Id > 0)
                {
                    string forgetPasswordTemplate = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.ForgetPasswordTemplate));
                    if (forgetPasswordTemplate.Length > 0)
                    {
                        userName = Utility.Encrypt(user.UserName);
                        subject = PatientPortalConstants.ConfigurationKeys.reSetPasswordSubject;
                        forgetPasswordLink = PatientPortalConstants.ConfigurationKeys.baseHref;
                        getTemplateString = forgetPasswordTemplate.Replace("%FirstName%", user.UserDetailDTO.FirstName).Replace("%RESETPASSWORDLINK%", forgetPasswordLink + "confirmpassword/" + userName);
                        string sendToList = user.UserName;
                        Utility.SendMail(subject, getTemplateString, sendToList, new List<string>(), attachedFiles, this.exceptionManager);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
            return user;
        }
        #endregion
    }
}
