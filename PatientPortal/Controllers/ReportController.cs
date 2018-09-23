using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult GetBillingReport()
        {
            return View();
        }

        public ActionResult ReportViewing()
        {
            return View();
        }
    }
}