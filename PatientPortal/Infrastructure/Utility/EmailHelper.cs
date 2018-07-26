using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PatientPortal.Infrastructure.Utility
{
    public class EmailHelper
    {
        public static string GetDeviceVerificationEmail(string firstname, string middlename, string lastname, string verificationCode)
        {
            string body = string.Format("Hi {0} {1} {2}<br/>", firstname, middlename, lastname);
            body += "As you requested, here is a OTP is : <b>" + verificationCode + "</b> you can use to verify your mobile number.<br/><br/>";
            body += "Thank You,<br/>";
            body += "Patient Portal Information System Customer Support";
            return body;
        }

        public static string GetRegistrationSuccessEmail(string firstname, string middlename, string lastname, string registrationnumber, string link)
        {
            string body = string.Format("Hi {0} {1} {2}<br/>", firstname, middlename, lastname);
            body += "As you requested, here registration is created, your registration number is : <b>" + registrationnumber + "</b> you can use to create your Password by clicking on below URL.<br/>";
            body += "<br/><b></b>" + link + "<br/><br/>";
            body += "Thank You,<br/>";
            body += "Patient Portal Information System Customer Support";
            return body;
        }
    }
}