using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PatientPortal.Infrastructure
{
    public class SMSService : IMessageSystem
    {
        public void Send(Message msg)
        {
           string _smsSenderId= Global.Utility.GetAppSettingKey("SmsSenderId");
            string _smsKey = Global.Utility.GetAppSettingKey("SmsKey");
            WebRequest request = WebRequest.Create("http://sms.eteknovation.com/app/smsapi/index.php?key="+ _smsKey + "&routeid=6&type=text&contacts="+msg.MessageTo+"&senderid="+ _smsSenderId + "&msg="+msg.Body) as HttpWebRequest;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();
        }
    }
}