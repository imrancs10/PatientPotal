using PatientPortal.BAL.Reports;
using PatientPortal.Global;
using PatientPortal.Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace PatientPortal.Controllers
{
    public class ReportController : CommonController
    {
        // GET: Report
        [HttpGet]
        public ActionResult GetBillingReport()
        {
            ReportDetails _details = new ReportDetails();
            List<DataLayer.PateintLeadger> result = _details.GetBillReportData();
            return View(result);
        }

        [HttpGet]
        public ActionResult DuplicateBillingReport()
        {
            ReportDetails _details = new ReportDetails();
            return View(_details.GetBillReportData());
        }

        public ActionResult ReportViewing()
        {
            ReportDetails _details = new ReportDetails();
            return View(_details.GetLabReportData());
        }

        public ActionResult PatientLedger()
        {
            ReportDetails _details = new ReportDetails();
            return View(_details.GetPatientLedger());
        }

        [HttpPost]
        public ActionResult FilterLeadgerReport(string DateFrom, string DateTo)
        {
            DateTime FromDate = DateTime.Now.AddMonths(-6);
            bool isOKfromdate = DateTime.TryParseExact(DateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
            if (isOKfromdate)
            {
                FromDate = result;
            }
            DateTime ToDate = DateTime.Now;
            bool isOKtodate = DateTime.TryParseExact(DateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime resultTo);
            if (isOKfromdate)
            {
                ToDate = resultTo;
            }
            int monthsApart = 12 * (FromDate.Year - ToDate.Year) + FromDate.Month - ToDate.Month;
            int diff = Math.Abs(monthsApart);
            if (diff > 6)
            {
                SetAlertMessage("Date Duration should between 6 month", "Leadger Report");
                return RedirectToAction("PatientLedger");
            }
            ReportDetails _details = new ReportDetails();
            List<Models.Patient.PatientLedgerModel> leaders = _details.GetPatientLedger(FromDate, ToDate);
            return View("PatientLedger", leaders);
        }

        public ActionResult DownloadReportFile(string fileUrl)
        {
            string _fileDirectory = fileUrl.Substring(0, fileUrl.LastIndexOf("\\") + 1);
            string _fileName = fileUrl.Substring(fileUrl.LastIndexOf("\\") + 1);
            if (Directory.Exists(_fileDirectory))
            {
                string[] files = Directory.GetFiles(_fileDirectory);
                if (files.Length > 0)
                {
                    string file = files.Where(x => x.Substring(x.LastIndexOf("\\") + 1) == _fileName).FirstOrDefault();
                    if (file != null)
                    {
                        byte[] FileBytes = System.IO.File.ReadAllBytes(file);
                        return File(FileBytes, "application/pdf", _fileName.Substring(0, _fileName.LastIndexOf('.')) + "-" + DateTime.Now.ToShortDateString() + ".pdf");
                    }
                }
            }

            return RedirectToAction("GetBillingReport");
        }

        public ActionResult ViewReportFile(string fileUrl, string _view)
        {
            string _fileDirectory = fileUrl.Substring(0, fileUrl.LastIndexOf("\\") + 1);
            string _fileName = fileUrl.Substring(fileUrl.LastIndexOf("\\") + 1);
            if (Directory.Exists(_fileDirectory))
            {
                string[] _files = Directory.GetFiles(_fileDirectory);
                if (_files.Where(x => x.Substring(fileUrl.LastIndexOf("\\") + 1) == _fileName).Count() > 0)
                {
                    byte[] _fileContent = System.IO.File.ReadAllBytes(fileUrl);
                    return File(_fileContent, "application/pdf");
                }
                return View(_view);
            }
            return RedirectToRoute(fileUrl);
        }

        public FileResult DownloadFile(string url, string ext)
        {
            WebClient client = new WebClient();
            //url = CryptoEngine.Decrypt(url.Replace("*", "="));
            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
            client.DownloadFile(url, downloadsPath + "/" + "DownloadLabPdf." + ext);

            byte[] fileBytes = System.IO.File.ReadAllBytes(downloadsPath + "/" + "DownloadLabPdf." + ext);
            FileContentResult response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = "labreport" + DateTime.Now.Date + "." + ext
            };
            return response;
        }

        public ActionResult ViewBillingReport(string Id,string type)
        {
            var url = ConfigurationManager.AppSettings["HISBillReportUrl"] + "?billid=" + CryptoEngine.Decrypt(Convert.ToString(Id)) + "&vtype=" + CryptoEngine.Decrypt(Convert.ToString(type));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader responseStream = new StreamReader(response.GetResponseStream());

            string resultado = responseStream.ReadToEnd();
            resultado = resultado.Replace("img/rmllogo.jpg", "http://103.78.201.146/healer/img/rmllogo.jpg");
            return Content(resultado); 
        }
        public FileStreamResult GetPDF(string path)
        {
            //Create a stream for the file
            Stream stream = null;

            //This controls how many bytes to read at a time and send to the client
            int bytesToRead = 10000;

            // Buffer to read bytes in chunk size specified above
            byte[] buffer = new Byte[bytesToRead];

            // The number of bytes read
            try
            {
                //Create a WebRequest to get the file
                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(path);

                //Create a response for this request
                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                if (fileReq.ContentLength > 0)
                    fileResp.ContentLength = fileReq.ContentLength;

                //Get the Stream returned from the response
                stream = fileResp.GetResponseStream();

                // prepare the response to the client. resp is the client Response
                var resp = HttpContext.Response;

                //Indicate the type of data being sent
                resp.ContentType = "application/octet-stream";

                //Name the file 
                string fileName = "test.pdf";
                resp.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                resp.AddHeader("Content-Length", fileResp.ContentLength.ToString());

                int length;
                do
                {
                    // Verify that the client is connected.
                    if (resp.IsClientConnected)
                    {
                        // Read data into the buffer.
                        length = stream.Read(buffer, 0, bytesToRead);

                        // and write it out to the response's output stream
                        resp.OutputStream.Write(buffer, 0, length);

                        // Flush the data
                        resp.Flush();

                        //Clear the buffer
                        buffer = new Byte[bytesToRead];
                    }
                    else
                    {
                        // cancel the download if client has disconnected
                        length = -1;
                    }
                } while (length > 0); //Repeat until no data is read
            }
            finally
            {
                if (stream != null)
                {
                    //Close the input stream
                    stream.Close();
                }
            }
            return File(stream, "application/pdf");
        }
    }
}