using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.BAL.Appointments;

namespace PatientPortal.Controllers
{
    public class AppointmentController : CommonController
    {
        // GET: Appointment
        public ActionResult GetAppointments()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DeptWiseDoctorScheduleList(int deptId=0,int year=0,int month=0)
        {
            AppointDetails _details = new AppointDetails();
            return Json(_details.DeptWiseDoctorScheduleList(deptId,year,month), JsonRequestBehavior.AllowGet);
        }
    }
}