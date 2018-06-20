using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.Global;

namespace PatientPortal.Controllers
{
    public class CommonController : Controller
    {
        public string LoginResponse(Enums.LoginMessage inputMessage)
        {
            if (inputMessage == Enums.LoginMessage.InvalidCreadential)
                return Global.Resource.Login_InvalidCredential;
            else
                return Global.Resource.Common_NoResponseFromServer;
        }

        public void SetAlertMessage(string message,string title="Alert")
        {
            TempData["Alert_Message"] = message;
            TempData["Alert_Title"] = title;
        }
    }
}