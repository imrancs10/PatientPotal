using PatientPortal.BAL.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult GetLabReport()
        {
            PatientDetails _details = new PatientDetails();
            return View(_details.GetReportList());
        }
    }
}