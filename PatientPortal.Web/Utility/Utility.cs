using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using PatientPortal.DTOModel;
using PatientPortal.Shared;

namespace PatientPortal.Web
{
    public static class Utility
    {
        /// <summary>
        /// Method is used to get response of error code
        /// </summary>
        /// <typeparam name="T">DTO entity</typeparam>
        /// <param name="objDTO">DTO</param>
        /// <returns>DTO entity</returns>
        public static T GetResultData<T>(T objDTO)
        {
            if (objDTO == null)
            {
                objDTO = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo propertyInfo = typeof(T).GetProperty(PatientPortalConstants.ApplicationKeys.ErrorCode);
                propertyInfo.SetValue(objDTO, 1000);
            }
            return objDTO;
        }

        /// <summary>
        /// Method is used to get response of error code
        /// </summary>
        /// <typeparam name="T">DTO entity</typeparam>
        /// <param name="objDTO">DTO</param>
        /// <returns>operation result type DTO entity</returns>
        public static OperationResult<T> GetResultData<T>(OperationResult<T> objDTO)
        {
            if (objDTO.ResultType != OperationResultType.Success)
            {
                if (objDTO.Data == null)
                {
                    objDTO.ErrorCode = 1000;
                }
            }
            return objDTO;
        }

        /// <summary>
        /// Set message whether it be success or failure or any other
        /// </summary>
        /// <typeparam name="T">Entity Name</typeparam>
        /// <param name="entity">Return entity object</param>
        public static int SetMessage<T>(OperationResult<T> entity)
        {
            switch (entity.ResultType)
            {
                case OperationResultType.Error:
                    entity.ErrorCode = (int)EnumMessage.Error;
                    break;

                default:
                    entity.ErrorCode = 0;
                    break;
            }
            return entity.ErrorCode;
        }

        /// <summary>
        /// Method is use to encrypt a string
        /// </summary>
        /// <param name="stringToBeEncrypt">String which would be encrypted</param>
        /// <returns>return encrypt string</returns>
        public static string Encrypt(string stringToBeEncrypt)
        {
            string encryptionKey = PatientPortalConstants.ConfigurationKeys.encryptionKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(stringToBeEncrypt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    stringToBeEncrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            return stringToBeEncrypt.Replace("/", "^^").Replace("+", "~~").Replace("=", "!5");
        }

        /// <summary>
        /// Method is use to decrypt a string
        /// </summary>
        /// <param name="stringToBeDecrypt">String which would be decrypted</param>
        /// <returns>return actual string</returns>
        public static string Decrypt(string stringToBeDecrypt)
        {
            stringToBeDecrypt = stringToBeDecrypt.Replace("^^", "/").Replace("~~", "+").Replace("!5", "=");
            string encryptionKey = PatientPortalConstants.ConfigurationKeys.encryptionKey;
            byte[] cipherBytes = Convert.FromBase64String(stringToBeDecrypt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    stringToBeDecrypt = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return stringToBeDecrypt;
        }


        /// <summary>
        /// This function is use to send mail to many recipeients
        /// </summary>
        /// <param name="subject">Subject of the mail</param>
        /// <param name="body">Body of the mail</param>
        /// <param name="sendTo">List of SEND to mails</param>
        /// <returns>Return bool type whether sent a mail or not</returns>
        public static bool SendMail(string subject, string body, string sendTo, List<string> sendCC, List<string> attachmentFilePath, IExceptionManager exceptionManager)
        {

            //Plugin.Log4net.Log4netLogger logger = new Plugin.Log4net.Log4netLogger();
            //logger.LogInfo(PatientPortalConstants.Messages.FileNotExisted);



            string smtpServer = string.Empty, fromEmailId = string.Empty, fromName = string.Empty;
            bool isSend = false;
            smtpServer = PatientPortalConstants.ConfigurationKeys.smtpServer;
            fromEmailId = PatientPortalConstants.ConfigurationKeys.fromEmailId;
            fromName = PatientPortalConstants.ConfigurationKeys.fromName;
            int port = Convert.ToInt16(PatientPortalConstants.ConfigurationKeys.port);

            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                if (!string.IsNullOrEmpty(sendTo))
                {
                    mail.To.Add(sendTo);
                    mail.From = new System.Net.Mail.MailAddress(fromEmailId, fromName);
                    foreach (string CCEmail in sendCC)
                    {
                        // Adding Multiple CC email Id
                        mail.CC.Add(new MailAddress(CCEmail));
                    }
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    mail.Body = body;
                    foreach (string filePath in attachmentFilePath)
                    {
                        // Check file if existed
                        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                        {
                            mail.Attachments.Add(new Attachment(filePath));
                        }
                        else
                        {
                            //Exception ex = new FileNotFoundException();
                            //ExplicitlyThrowError(exceptionManager, ex, filePath + PatientPortalConstants.Messages.FileNotExisted);
                            //Plugin.Log4net.Log4netLogger logger = new Plugin.Log4net.Log4netLogger();
                            //logger.LogDebug(PatientPortalConstants.Messages.FileNotExisted);

                        }
                    }
                    System.Net.Mail.SmtpClient smtpMailObj = new System.Net.Mail.SmtpClient(smtpServer);
                    smtpMailObj.Port = port;
                    smtpMailObj.Send(mail);
                    mail.Dispose();
                    isSend = true;
                }
            }
            catch (Exception ex)
            {
                exceptionManager.HandleException(ex, ex.Message);
            }

            return isSend;
        }

        /// <summary>
        /// Set culture for application
        /// </summary>
        public static void SetCulture(int languageId)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("Language");
            cookie.Value = Enum.GetName(typeof(EnumLanguage), languageId).Replace("_", "-"); ;
            cookie.Expires = DateTime.Now.AddDays(1);
            System.Web.HttpContext.Current.Response.SetCookie(cookie);
            System.Web.HttpContext.Current.Request.Cookies.Add(cookie);
            CultureInfo ci = new CultureInfo(cookie.Value);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci; System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        }

        /// <summary>
        /// Set user session
        /// </summary>
        /// <param name="userDTO">user object</param>
        public static void SetUserSession(UserDTO userDTO)
        {
            System.Web.HttpContext.Current.Session["User"] = userDTO;
        }

        ///// <summary>
        ///// Delete PDF files before today date
        ///// </summary>
        //public static void DeletePDFFiles()
        //{
        //    DateTime currentDate = Convert.ToDateTime(System.DateTime.Now.ToString("MM-dd-yyyy"));
        //    DirectoryInfo directory = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder));

        //    foreach (var file in directory.GetFiles("*.pdf"))
        //    {
        //        if (file.Name.Contains(PatientPortalConstants.FileName.RecommendedProduct))
        //        {
        //            string[] splitFileName = file.Name.Split('-');
        //            if (splitFileName.Length > 0)
        //            {
        //                DateTime getFileName = Convert.ToDateTime((splitFileName[2].Length == 1 ? ("0" + splitFileName[2]) : splitFileName[2]) + "-" + (splitFileName[3].Length == 1 ? ("0" + splitFileName[3]) : splitFileName[3]) + "-" + splitFileName[4]);
        //                if (getFileName.Date < currentDate.Date)
        //                {
        //                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder + "/" + file.Name));
        //                    if (fileInfo.Exists)
        //                    {
        //                       // fileInfo.Delete();
        //                    }
        //                }
        //            }
        //        }
        //        if (file.Name.Contains(PatientPortalConstants.FileName.SurveyResult))
        //        {
        //            //Nagarro Test CR_adsf_adf_SurveyResult 4-4-2016-32-8.pdf
        //            int lastSpaceIndex = file.Name.LastIndexOf(' ');
        //            string[] splitFileName = file.Name.Substring(lastSpaceIndex + 1).Split('-');
        //            if (splitFileName.Length > 0)
        //            {
        //                string strDate = (splitFileName[0].Length == 1 ? ("0" + splitFileName[0]) : splitFileName[0]) + "-" + (splitFileName[1].Length == 1 ? ("0" + splitFileName[1]) : splitFileName[1]) + "-" + splitFileName[2];
        //                DateTime getFileName = DateTime.ParseExact(strDate, "MM/dd/YYYY", null);
        //               // DateTime getFileName = Convert.ToDateTime();
        //                if (getFileName.Date < currentDate.Date)
        //                {
        //                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath(PatientPortalConstants.FilePath.PDFFileFolder + "/" + file.Name));
        //                    if (fileInfo.Exists)
        //                    {
        //                       // fileInfo.Delete();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}