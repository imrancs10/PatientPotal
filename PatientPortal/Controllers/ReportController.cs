using PatientPortal.BAL.Reports;
using PatientPortal.Infrastructure.Utility;
using System;
using System.Collections.Generic;
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

        public FileResult DownloadFile(string url)
        {
            WebClient client = new WebClient();
            url = url.Replace("$", "/");
            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads);
            client.DownloadFile(url, downloadsPath + "DownloadLabPdf.pdf");

            byte[] fileBytes = System.IO.File.ReadAllBytes(downloadsPath + "DownloadLabPdf.pdf");
            FileContentResult response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = "labreport" + DateTime.Now.Date + ".pdf"
            };
            return response;
        }
    }
}