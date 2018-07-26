using com.awl.MerchantToolKit;
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
                        OTP = verificationCode,
                        Subject = "Verify Mobile Number",
                        Body = EmailHelper.GetDeviceVerificationEmail(firstname, middlename, lastname, verificationCode)
                    };
                    ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                    sendMessageStrategy.SendMessages();
                    Session["otp"] = verificationCode;
                    Session["PatientId"] = ((PatientInfo)result["data"]).PatientId;
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
            string baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                ReqMsgDTO objReqMsgDTO;
                objReqMsgDTO = new ReqMsgDTO();
                objReqMsgDTO.OrderId = VerificationCodeGeneration.GenerateDeviceVerificationCode();
                objReqMsgDTO.Mid = MerchantId;
                objReqMsgDTO.Enckey = EncryptKey;
                objReqMsgDTO.MeTransReqType = "S";
                objReqMsgDTO.TrnAmt = TransactionAmount;
                objReqMsgDTO.RecurrPeriod = "";
                objReqMsgDTO.RecurrDay = "";
                objReqMsgDTO.ResponseUrl = baseUrl + ResponseUrl;
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
                AWLMEAPI objawlmerchantkit = new AWLMEAPI();
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
            return View();
        }
        [HttpGet]
        public ActionResult CreatePassword(string registrationNumber)
        {
            ViewData["registrationNumber"] = registrationNumber;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePassword(string password, string confirmpassword,string registrationNumber)
        {
            if (password.Trim() != confirmpassword.Trim())
            {
                SetAlertMessage("Password and Confirm Password are not match", "password Create");
                return View();
            }
            else
            {
                PatientDetails _details = new PatientDetails();
                var result = _details.GetPatientDetailByRegistrationNumber(registrationNumber);
                if (result != null)
                {
                    result.Password = password.Trim();
                    _details.UpdatePatientDetail(result);
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlertMessage("Your Registration Number is Incorrect,Kindly contact the administrator", "password Create");
                    return View();
                }
            }
        }
        public ActionResult TransactionResponse()
        {
            string EncryptKey = Convert.ToString(ConfigurationManager.AppSettings["EncryptKey"]);
            ResMsgDTO objResMsgDTO = new ResMsgDTO();
            if (Request.Form["merchantResponse"] != null)
            {

                string merchantResponse = Request.Form["merchantResponse"];
                AWLMEAPI transact = new AWLMEAPI();
                objResMsgDTO = transact.parseTrnResMsg(merchantResponse, EncryptKey);

                PatientDetails _details = new PatientDetails();
                string serialNumber = VerificationCodeGeneration.GetSerialNumber();
                int patientId = (int)Session["PatientId"];
                PatientInfo info = new PatientInfo()
                {
                    RegistrationNumber = serialNumber,
                    PatientId = patientId
                };
                info = _details.UpdatePatientDetail(info);

                PatientTransaction transaction = new PatientTransaction()
                {
                    PatientId = info.PatientId,
                    Amount = Convert.ToInt32(objResMsgDTO.TrnAmt),
                    OrderId = objResMsgDTO.OrderId,
                    ResponseCode = objResMsgDTO.ResponseCode,
                    StatusCode = objResMsgDTO.StatusCode,
                    TransactionDate = Convert.ToDateTime(objResMsgDTO.TrnReqDate),
                    TransactionNumber = objResMsgDTO.PgMeTrnRefNo
                };
                _details.SavePatientTransaction(transaction);

                string passwordCreateURL = "Home/CreatePassword?registrationNumber=" + serialNumber;
                string baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                Message msg = new Message()
                {
                    MessageTo = info.Email,
                    MessageNameTo = info.FirstName + " " + info.MiddleName + (string.IsNullOrWhiteSpace(info.MiddleName) ? "" : " ") + info.LastName,
                    Subject = "Registration Created",
                    Body = EmailHelper.GetRegistrationSuccessEmail(info.FirstName, info.MiddleName, info.LastName, serialNumber, baseUrl + passwordCreateURL)
                };

                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
                transaction.OrderId = serialNumber;
                ViewData["TransactionSuccessResult"] = transaction;
                return View();
            }
            else
            {
                SetAlertMessage("There Was Some Error in transaction Processing.....Please Check The Data you have Entered", "Transaction");
                return View();
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