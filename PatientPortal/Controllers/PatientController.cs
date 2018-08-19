using PatientPortal.BAL.Patient;
using PatientPortal.Global;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View(_details.GetReportList(Convert.ToInt32(Session["PatientId"])));
        }

        public ActionResult GetPDF(string fileName)
        {
            string dirUrl = "~/LabReports/" + WebSession.PatientRegNo;
            string dirPath = Server.MapPath(dirUrl);
            // Check for Directory, If not exist, then create it  
            if (Directory.Exists(dirPath))
            {
                string[] files = Directory.GetFiles(dirPath);
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        string extractfileName = file.Substring(file.LastIndexOf("\\") + 1);
                        if (extractfileName == fileName)
                        {
                            byte[] FileBytes = System.IO.File.ReadAllBytes(file);
                            return File(FileBytes, "application/pdf");
                        }
                    }
                }
            }

            return RedirectToAction("GetLabReport");
        }
    }
}