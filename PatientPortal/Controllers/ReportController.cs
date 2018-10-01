using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.BAL;
using PatientPortal.BAL.Reports;
using System.IO;
using PatientPortal.BAL.Commom;

namespace PatientPortal.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult GetBillingReport()
        {
            ReportDetails _details = new ReportDetails();
            return View(_details.GetBillReportData());
        }

        public ActionResult ReportViewing()
        {
            ReportDetails _details = new ReportDetails();
            return View(_details.GetLabReportData());
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
                    var file = files.Where(x => x.Substring(x.LastIndexOf("\\") + 1) == _fileName).FirstOrDefault();
                    if (file != null)
                    {
                        byte[] FileBytes = System.IO.File.ReadAllBytes(file);
                        return File(FileBytes, "application/pdf", _fileName.Substring(0, _fileName.LastIndexOf('.')) + "-" + DateTime.Now.ToShortDateString() + ".pdf");
                    }
                }
            }

            return RedirectToAction("GetBillingReport");
        }
        public FileResult GetReport(string url)
        {
            string ReportURL = @"D:\Personnal\PatientPortal\WorkingCode\PatientPotal\PatientPortal\\Reports\Bill\2018\November\2.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }
    }
}