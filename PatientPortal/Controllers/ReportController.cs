using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.BAL;
using PatientPortal.BAL.Reports;

namespace PatientPortal.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult GetBillingReport()
        {
            return View();
        }

        public ActionResult GetBillingReportData()
        {
            ReportDetails _details = new ReportDetails();
            return View("GetBillingReport", _details.GetBillReportData());
        }

        public ActionResult ReportViewing()
        {
            return View();
        }
    }
}