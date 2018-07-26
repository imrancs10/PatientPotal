using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.BAL.Appointments;
using DataLayer;

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

        [HttpPost]
        public JsonResult DayWiseDoctorScheduleList(int deptId, string day)
        {
            AppointDetails _details = new AppointDetails();
            return Json(_details.DayWiseDoctorScheduleList(deptId,day), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAppointment(AppointmentInfo model)
        {
            AppointDetails _details = new AppointDetails();
            model.PatientId = Convert.ToInt32(Session["PatientId"].ToString());
            return Json(CrudResponse(_details.SaveAppointment(model)), JsonRequestBehavior.AllowGet);
        }
    }
}