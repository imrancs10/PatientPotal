﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.BAL.Appointments;
using DataLayer;
using PatientPortal.Infrastructure;
using PatientPortal.Infrastructure.Utility;
using PatientPortal.Global;

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
        public JsonResult DeptWiseDoctorScheduleList(int deptId = 0, int year = 0, int month = 0)
        {
            AppointDetails _details = new AppointDetails();
            return Json(_details.DeptWiseDoctorScheduleList(deptId, year, month), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DayWiseDoctorScheduleList(int deptId, string day)
        {
            AppointDetails _details = new AppointDetails();
            return Json(_details.DayWiseDoctorScheduleList(deptId, day), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DateWiseDoctorAppointmentList(DateTime date)
        {
            AppointDetails _details = new AppointDetails();
            return Json(_details.DateWiseDoctorAppointmentList(date), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAppointment(AppointmentInfo model, string doctorname, string deptname)
        {
            AppointDetails _details = new AppointDetails();
            model.PatientId = Convert.ToInt32(Session["PatientId"].ToString());
            //PatientInfo data = (PatientInfo)Session["PatientData"];
            var user = User;
            Message msg = new Message()
            {
                MessageTo = user.Email,
                MessageNameTo = user.FirstName + " " + user.MiddleName + (string.IsNullOrWhiteSpace(user.MiddleName) ? "" : " ") + user.LastName,
                Subject = "Appointment Booking Confirmation",
                Body = EmailHelper.GetAppointmentSuccessEmail(user.FirstName, user.MiddleName, user.LastName, doctorname, model.AppointmentDateFrom, deptname)
            };
            ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
            sendMessageStrategy.SendMessages();
            return Json(CrudResponse(_details.SaveAppointment(model)), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPatientAppointmentList()
        {
            AppointDetails _details = new AppointDetails();
            int _patientId = 0;
            string _sessionPatienId = Session["PatientId"]==null?"0": Session["PatientId"].ToString();
            Dictionary<int, string> result = new Dictionary<int, string>();
            if (int.TryParse(_sessionPatienId, out _patientId))
            {                
                return Json(_details.PatientAppointmentList(_patientId), JsonRequestBehavior.AllowGet);
            }
            else
            {
                result.Add((int)Enums.JsonResult.Invalid_DataId, "Patient Id is invalid");
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult CancelAppointment(int appointmentId,string CancelReason)
        {
            AppointDetails _details = new AppointDetails();
            int PatientId = 0;
            Dictionary<int, string> result = new Dictionary<int, string>();
            if (int.TryParse(Session["PatientId"].ToString(), out PatientId))
            {
                return Json(_details.CancelAppointment(PatientId, appointmentId,CancelReason).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                result.Add((int)Enums.JsonResult.Invalid_DataId, "Patient Id is invalid");
                return Json(result.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PatientProfile()
        {
            return View();
        }
    }
}