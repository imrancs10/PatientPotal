using PatientPortal.BAL.Reports;
using PatientPortal.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
            result.ForEach(x =>
            {
                x.netamt = Math.Round(x.netamt.Value, 2);
            });
            return View(result);
        }

        [HttpGet]
        public ActionResult DuplicateBillingReport()
        {
            ReportDetails _details = new ReportDetails();
            var result = _details.GetBillReportData();
            result.ForEach(x =>
            {
                x.netamt = Math.Round(x.netamt.Value, 2);
            });
            return View(result);
        }

        public ActionResult ReportViewing()
        {
            ReportDetails _details = new ReportDetails();
            var result = _details.GetLabReportData();
            return View(result);
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

        public ActionResult DownloadFile(string Id)
        {
            string url = ConfigurationManager.AppSettings["HISLabReportUrl"] + "/" + CryptoEngine.Decrypt(Convert.ToString(Id)) + ".pdf";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader responseStream = new StreamReader(response.GetResponseStream());

            var ms = new MemoryStream();
            responseStream.BaseStream.CopyTo(ms);

            var imageBytes = ms.ToArray();

            FileContentResult responseFile = new FileContentResult(imageBytes, "application/octet-stream")
            {
                FileDownloadName = "labreport" + DateTime.Now.Date + ".pdf"
            };
            return responseFile;
        }

        public ActionResult ViewBillingReport(string Id, string type)
        {
            var url = ConfigurationManager.AppSettings["HISBillReportUrl"] + "?billid=" + CryptoEngine.Decrypt(Convert.ToString(Id)) + "&vtype=" + CryptoEngine.Decrypt(Convert.ToString(type));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader responseStream = new StreamReader(response.GetResponseStream());

            string resultado = responseStream.ReadToEnd();
            resultado = resultado.Replace("img/rmllogo.jpg",
                                           ConfigurationManager.AppSettings["HISBillReportBaseUrl"] + "/img/rmllogo.jpg");
            return Content(resultado);
        }
        public ActionResult ViewLabReport(string Id)
        {
            string url = ConfigurationManager.AppSettings["HISLabReportUrl"] + "/" + CryptoEngine.Decrypt(Convert.ToString(Id)) + ".pdf";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader responseStream = new StreamReader(response.GetResponseStream());

            var ms = new MemoryStream();
            responseStream.BaseStream.CopyTo(ms);

            var imageBytes = ms.ToArray();
            return File(imageBytes, response.ContentType);
        }

        public ActionResult ViewRadiologyLabReport(string Id)
        {
            string url = ConfigurationManager.AppSettings["HISRadiologyReportUrl"] + "?labrefno=" + CryptoEngine.Decrypt(Convert.ToString(Id));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader responseStream = new StreamReader(response.GetResponseStream());

            var ms = new MemoryStream();
            responseStream.BaseStream.CopyTo(ms);

            var imageBytes = ms.ToArray();
            return File(imageBytes, response.ContentType);
        }
        [HttpPost]
        public ActionResult PatientLedgerPaymentReport()
        {
            ReportDetails _details = new ReportDetails();
            //return View(_details.GetPatientLedger());
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var ledgerData = _details.GetPatientLedger().Where(x => x.Type == "GP" || x.Type == "GR").ToList();
            ledgerData.ForEach(x =>
            {
                if (x.Type == "GP")
                {
                    x.Receipt = "";
                }
                else
                {
                    x.Payment = "";
                }
            });
            //SORT
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
            //    //v = v.OrderBy(sortColumn + " " + sortColumnDir);
            //    ledgerData = ledgerData.OrderByDescending(x => x.Date).ToList();
            //}

            recordsTotal = ledgerData.Count();
            var data = ledgerData.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PatientLedgerFormacyReport()
        {
            ReportDetails _details = new ReportDetails();
            //return View(_details.GetPatientLedger());
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var ledgerData = _details.GetPatientLedger().Where(x => x.Type == "PH" || x.Type == "SV" || x.Type == "PHR" || x.Type == "SR" || x.Type == "RS").ToList();
            ledgerData.ForEach(x =>
            {
                if (x.Type == "PH" || x.Type == "SV" || x.Type == "SP")
                {
                    x.Receipt = "";
                }
                else
                {
                    x.Payment = "";
                }
            });

            recordsTotal = ledgerData.Count();
            var data = ledgerData.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}