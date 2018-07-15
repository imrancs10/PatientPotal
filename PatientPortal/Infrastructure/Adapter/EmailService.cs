using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using PatientPortal.Infrastructure.Utility;

namespace PatientPortal.Infrastructure
{
    public class EmailService : IMessageSystem
    {
        public void Send(Message msg)
        {
            //notify user via email
            string hostEmail = Convert.ToString(ConfigurationManager.AppSettings["HostEmail"]);
            string HostEmailName = Convert.ToString(ConfigurationManager.AppSettings["HostEmailName"]);
            string HostEmailPassword = Convert.ToString(ConfigurationManager.AppSettings["HostEmailPassword"]);
            string HostAddress = Convert.ToString(ConfigurationManager.AppSettings["HostAddress"]);
            int HostPort = Convert.ToInt32(ConfigurationManager.AppSettings["HostPort"]);


            var fromAddress = new MailAddress(hostEmail, HostEmailName);
            var toAddress = new MailAddress(msg.MessageTo, string.IsNullOrEmpty(msg.MessageNameTo) ? "User" : msg.MessageNameTo);
            const string subject = "Verify Mobile Number";
            string body = "Hi ," +
                                "As you requested, here is a OTP : " + msg.OTP + " you can use to verify your mobile number." +
                                "If you need further assistance, please call our 24 x 7 call center toll free at 000-000-0000." +
                                "Thank You," +
                                "Patient Portal Information System Customer Support";
            var smtp = new SmtpClient
            {
                Host = HostAddress,
                Port = HostPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, HostEmailPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.SendCompleted += (s, e) =>
                {
                    smtp.Dispose();
                    message.Dispose();
                };
                smtp.Send(message);
            }
        }
    }
}