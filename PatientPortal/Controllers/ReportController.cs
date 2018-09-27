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
    }
}