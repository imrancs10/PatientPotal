using DataLayer;
using PatientPortal.BAL.Login;
using PatientPortal.BAL.Patient;
using PatientPortal.Global;
using PatientPortal.Infrastructure;
using PatientPortal.Infrastructure.Authentication;
using PatientPortal.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using static PatientPortal.Global.Enums;

namespace PatientPortal.Controllers
{
    public class HomeController : CommonController
    {
        // GET: Home
        [CustomAuthorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Index(string actionName)
        {
            if (actionName != "otp")
            {
                ViewData["LoginAction"] = "OTP";
            }
            return View();
        }

        [MultipleButton(Name = "action", Argument = "getotp")]
        [HttpPost]
        public ActionResult GetPatientOTP(string username, string password)
        {
            PatientDetails _details = new PatientDetails();
            var result = _details.GetPatientDetail(username, password);
            if (result != null)
            {
                string verificationCode = VerificationCodeGeneration.GenerateDeviceVerificationCode();
                Message msg = new Message() { MessageTo = result.Email, MessageNameTo = result.FirstName + " " + result.MiddleName + (string.IsNullOrWhiteSpace(result.MiddleName) ? "" : " ") + result.LastName, OTP = verificationCode };
                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
                PatientInfo info = new PatientInfo() { PatientId = result.PatientId, OTP = verificationCode };
                _details.UpdatePatientDetail(info);
                Session["PatientId"] = result.PatientId;
                return RedirectToAction("Index", new { actionName = "otp" });
            }
            return View("Index");
        }
        [MultipleButton(Name = "action", Argument = "getlogin")]
        [HttpPost]
        public ActionResult GetPatientLogin(string OTP)
        {
            PatientDetails _details = new PatientDetails();
            var result = _details.VerifyPatientOTP(Convert.ToInt32(Session["PatientId"]), OTP);
            if (result)
            {
                PatientInfo info = new PatientInfo() { PatientId = Convert.ToInt32(Session["PatientId"]), OTP = null };
                info = _details.UpdatePatientDetail(info);
                Session["PatientId"] = null;
                setUserClaim(info);
                return RedirectToAction("Dashboard");
            }
            else
            {
                SetAlertMessage("OTP is not match", "Login Response");
                return RedirectToAction("Index", new { actionName = "otp" });
            }
        }
        private void setUserClaim(PatientInfo info)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = info.PatientId;
            serializeModel.FirstName = info.FirstName;
            serializeModel.MiddleName = info.MiddleName;
            serializeModel.LastName = info.LastName;
            serializeModel.Email = info.Email;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     info.Email,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(string registermobile, string registerpassword, string registerconfirmpassword, string registeremail)
        {
            string emailRegEx = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (registermobile.Trim().Length != 10)
            {
                SetAlertMessage("Please Enter correct Mobile Number", "Register");
                return RedirectToAction("Index");
            }
            else if (registerpassword.Trim() != registerconfirmpassword.Trim())
            {
                SetAlertMessage("Confirm Password not match with Password", "Register");
                return RedirectToAction("Index");
            }
            else if (!Regex.IsMatch(registeremail, emailRegEx, RegexOptions.IgnoreCase))
            {
                SetAlertMessage("Please Enter correct Email Address", "Register");
                return RedirectToAction("Index");
            }
            else
            {
                PatientDetails _details = new PatientDetails();
                PatientInfo info = new PatientInfo()
                {
                    MobileNumber = registermobile.Trim(),
                    Password = registerpassword.Trim(),
                    Email = registeremail.Trim()
                };
                var result = _details.RegisterPatientDetail(info);
                if (result["status"].ToString() == CrudStatus.Saved.ToString())
                {
                    setUserClaim((PatientInfo)result["data"]);
                    
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    SetAlertMessage("User is already register", "Register");
                    return RedirectToAction("Index");
                }
            }
        }
    }
}