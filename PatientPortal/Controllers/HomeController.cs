using DataLayer;
using PatientPortal.BAL.Login;
using PatientPortal.BAL.Patient;
using PatientPortal.Global;
using PatientPortal.Infrastructure;
using PatientPortal.Infrastructure.Authentication;
using PatientPortal.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        [CustomAuthorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPatientLogin(string username, string password)
        {
            PatientDetails _details = new PatientDetails();
            var result = _details.GetPatientDetail(username, password);
            if (result != null)
            {
                Session["PatientId"] = result.PatientId;
                setUserClaim(result);
                return RedirectToAction("Dashboard");
            }
            else
            {
                SetAlertMessage("User Not Found", "Login");
                return View("Index");
            }
        }

        public ActionResult Register(string actionName)
        {
            if (actionName != "otp")
            {
                ViewData["LoginAction"] = "OTP";
            }
            return View();
        }

        [MultipleButton(Name = "action", Argument = "getotp")]
        [HttpPost]
        public ActionResult GetPatientOTP(string firstname, string middlename, string lastname, string DOB, string Gender, string mobilenumber, string email, string address, string city, string country, string pincode, string religion, string department)
        {
            string emailRegEx = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (mobilenumber.Trim().Length != 10)
            {
                SetAlertMessage("Please Enter correct Mobile Number", "Register");
                return RedirectToAction("Index");
            }
            else if (!Regex.IsMatch(email, emailRegEx, RegexOptions.IgnoreCase))
            {
                SetAlertMessage("Please Enter correct Email Address", "Register");
                return RedirectToAction("Index");
            }
            else
            {
                string verificationCode = VerificationCodeGeneration.GenerateDeviceVerificationCode();
                Dictionary<string, object> result = SavePatientInfo(firstname, middlename, lastname, DOB, Gender, mobilenumber, email, address, city, country, pincode, religion, department, verificationCode);
                if (result["status"].ToString() == CrudStatus.Saved.ToString())
                {
                    Message msg = new Message()
                    {
                        MessageTo = email,
                        MessageNameTo = firstname + " " + middlename + (string.IsNullOrWhiteSpace(middlename) ? "" : " ") + lastname,
                        OTP = verificationCode
                    };
                    ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                    sendMessageStrategy.SendMessages();
                    Session["otp"] = verificationCode;
                    return RedirectToAction("Register", new { actionName = "otp" });
                }
                else
                {
                    SetAlertMessage("User is already register", "Register");
                    return RedirectToAction("Register");
                }
            }
        }

        [MultipleButton(Name = "action", Argument = "verifyOTP")]
        [HttpPost]
        public ActionResult verifyOTP(string OTP)
        {
            if (Convert.ToString(Session["otp"]) == OTP)
            {
                return PaymentTransaction();
            }
            else
            {
                SetAlertMessage("OTP not matched", "Register");
                return RedirectToAction("Register");
            }
        }

        private ActionResult PaymentTransaction()
        {
            string MerchantId = Convert.ToString(ConfigurationManager.AppSettings["MerchantId"]);
            string EncryptKey = Convert.ToString(ConfigurationManager.AppSettings["EncryptKey"]);
            string TransactionAmount = Convert.ToString(ConfigurationManager.AppSettings["TransactionAmount"]);
            string ResponseUrl = Convert.ToString(ConfigurationManager.AppSettings["ResponseUrl"]);
            try
            {
                com.awl.MerchantToolKit.ReqMsgDTO objReqMsgDTO;
                objReqMsgDTO = new com.awl.MerchantToolKit.ReqMsgDTO();
                objReqMsgDTO.OrderId = VerificationCodeGeneration.GenerateDeviceVerificationCode();
                objReqMsgDTO.Mid = MerchantId;
                objReqMsgDTO.Enckey = EncryptKey;
                objReqMsgDTO.MeTransReqType = "S";
                objReqMsgDTO.TrnAmt = TransactionAmount;
                objReqMsgDTO.RecurrPeriod = "";
                objReqMsgDTO.RecurrDay = "";
                objReqMsgDTO.ResponseUrl = ResponseUrl;
                objReqMsgDTO.TrnRemarks = "Test";
                objReqMsgDTO.TrnCurrency = "INR";
                objReqMsgDTO.AddField1 = "";
                objReqMsgDTO.AddField2 = "";
                objReqMsgDTO.AddField3 = "";
                objReqMsgDTO.AddField4 = "";
                objReqMsgDTO.AddField5 = "";
                objReqMsgDTO.AddField6 = "";
                objReqMsgDTO.AddField7 = "";
                objReqMsgDTO.AddField8 = "";
                string Message;
                com.awl.MerchantToolKit.AWLMEAPI objawlmerchantkit = new com.awl.MerchantToolKit.AWLMEAPI();
                objawlmerchantkit.generateTrnReqMsg(objReqMsgDTO);
                Message = objReqMsgDTO.ReqMsg;
                Session["Message"] = Message;
                Session["MID"] = objReqMsgDTO.Mid;
                return RedirectToAction("TransactionPay");
            }
            catch (Exception ex)
            {
                SetAlertMessage("There Was Some Error Processing.....Please Check The Data you have Entered", "Transaction");
                return RedirectToAction("Register");
            }
        }

        public ActionResult TransactionPay()
        {
            //ViewData["TransactionMesage"] = "<input type=\"hidden\" name=\"merchantRequest\" id=\"merchantRequest\" value=\"" + Session["Message"] + "\"     />		<input type=\"hidden\" name=\"MID\" id=\"MID\" value=" + Session["MID"] + " /> ";

            return View();
        }

        public ActionResult TransactionResponse(object result)
        {
            return View();
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

        public ActionResult MyProfile()
        {

            PatientDetails _details = new PatientDetails();
            if (User == null)
            {
                SetAlertMessage("User has been logged out", "Update Profile");
                return RedirectToAction("Index");
            }
            var result = _details.GetPatientDetailById(User.Id);
            if (result == null)
            {
                SetAlertMessage("User not found", "Update Profile");
                return RedirectToAction("Index");
            }
            else
            {
                ViewData.Model = result;
            }
            return View();
        }
        private static Dictionary<string, object> SavePatientInfo(string firstname, string middlename, string lastname, string DOB, string Gender, string mobilenumber, string email, string address, string city, string country, string pincode, string religion, string department, string verificationCode)
        {
            PatientDetails _details = new PatientDetails();
            PatientInfo info = new PatientInfo()
            {
                FirstName = firstname,
                MiddleName = middlename,
                LastName = lastname,
                DOB = Convert.ToDateTime(DOB),
                Gender = Gender,
                MobileNumber = mobilenumber.Trim(),
                Email = email.Trim(),
                Address = address,
                City = city,
                Country = country,
                PinCode = Convert.ToInt32(pincode),
                Religion = religion,
                OTP = verificationCode,
                DepartmentId = Convert.ToInt32(department)
            };
            var result = _details.RegisterPatientDetail(info);
            return result;
        }
    }
}