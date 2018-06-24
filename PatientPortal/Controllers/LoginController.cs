using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.BAL.Login;
using PatientPortal.Global;

namespace PatientPortal.Controllers
{
    public class LoginController : CommonController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLogin(string username,string password)
        {
            LoginDetails _details = new LoginDetails();
            string _response = string.Empty;
            Enums.LoginMessage message=_details.GetLogin(username, password);
            _response = LoginResponse(message);
            if (message == Enums.LoginMessage.Authenticated)
            {
                return View("~/Views/home/dashboard.cshtml");
            }
            else
            {
                SetAlertMessage(_response, "Login Response");
                return View("index");
            }
        }
    }
}