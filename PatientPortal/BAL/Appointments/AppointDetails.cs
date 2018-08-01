using DataLayer;
using PatientPortal.Global;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PatientPortal.BAL.Appointments
{
    public class AppointDetails
    {
        PatientPortalEntities _db = null;
        public IEnumerable<object> DeptWiseDoctorScheduleList(int deptId = 0, int year = 0, int month = 0)
        {
            _db = new PatientPortalEntities();
            year = year == 0 ? DateTime.Now.Year : year;
            month = month == 0 ? DateTime.Now.Month : month;
            var _list = (from docSchedule in _db.DoctorSchedules

                         orderby docSchedule.DayID
                         where deptId == 0 || docSchedule.Doctor.DepartmentID.Equals(deptId)
                         select new
                         {
                             DayId = docSchedule.DayID,
                             docSchedule.DayMaster.DayName,
                             docSchedule.DoctorID,
                             docSchedule.Doctor.DoctorName,
                             docSchedule.Doctor.Department.DepartmentName,
                             docSchedule.DoctorScheduleID,
                             TimeFrom = docSchedule.TimeFrom + (docSchedule.TimeFromMeridiemID == 1 ? ":00 AM" : ":00 PM"),
                             TimeTo = docSchedule.TimeTo + (docSchedule.TimeToMeridiemID == 1 ? ":00 AM" : ":00 PM"),
                             docSchedule.TimeFromMeridiemID,
                             docSchedule.TimeToMeridiemID
                         }).GroupBy(x => x.DayName).ToList();
            return _list;
        }
        public IEnumerable<object> DayWiseDoctorScheduleList(int deptId, string day)
        {
            _db = new PatientPortalEntities();
            var _list = (from docSchedule in _db.DoctorSchedules

                         orderby docSchedule.DoctorID
                         where docSchedule.Doctor.DepartmentID.Equals(deptId) && docSchedule.DayMaster.DayName.ToLower().Equals(day.ToLower())
                         select new
                         {
                             DayId = docSchedule.DayID,
                             docSchedule.DayMaster.DayName,
                             docSchedule.DoctorID,
                             docSchedule.Doctor.DoctorName,
                             docSchedule.Doctor.Department.DepartmentName,
                             docSchedule.DoctorScheduleID,
                             TimeFrom = docSchedule.TimeFrom + (docSchedule.TimeFromMeridiemID == 1 ? ":00 AM" : ":00 PM"),
                             TimeTo = docSchedule.TimeTo + (docSchedule.TimeToMeridiemID == 1 ? ":00 AM" : ":00 PM"),
                             docSchedule.TimeFromMeridiemID,
                             docSchedule.TimeToMeridiemID
                         }).GroupBy(x => x.DoctorName).ToList();
            return _list;
        }
        public IEnumerable<object> DateWiseDoctorAppointmentList(DateTime date)
        {
            _db = new PatientPortalEntities();
            var _list = (from docAppointment in _db.AppointmentInfoes

                         orderby docAppointment.DoctorId
                         where DbFunctions.TruncateTime(docAppointment.AppointmentDateFrom) <= date && DbFunctions.TruncateTime(docAppointment.AppointmentDateFrom) >= date.Date
                         select new
                         {
                             docAppointment.AppointmentDateFrom,
                             docAppointment.AppointmentDateTo,
                             docAppointment.AppointmentId,
                             docAppointment.DoctorId,
                             docAppointment.Doctor.DoctorName,
                             docAppointment.PatientId
                         }).OrderBy(x => x.AppointmentDateFrom).GroupBy(x => x.DoctorId).ToList();
            return _list;
        }
        public Enums.CrudStatus SaveAppointment(AppointmentInfo model)
        {
            _db = new PatientPortalEntities();
            int _effectRow = 0;
            var _deptRow = _db.AppointmentInfoes.Where(x => x.AppointmentId.Equals(model.AppointmentId)).FirstOrDefault();
            if (_deptRow == null)
            {
                AppointmentInfo _newAppointment = new AppointmentInfo();
                _newAppointment.AppointmentDateFrom = model.AppointmentDateFrom;
                _newAppointment.AppointmentDateTo = model.AppointmentDateTo;
                _newAppointment.CreatedDate = DateTime.Now;
                _newAppointment.DoctorId = model.DoctorId;
                _newAppointment.PatientId = model.PatientId;
                _db.Entry(_newAppointment).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
        public IEnumerable<object> PatientAppointmentList(int _patientId)
        {
            _db = new PatientPortalEntities();
            var _list = (from docAppointment in _db.AppointmentInfoes

                         orderby docAppointment.DoctorId
                         where docAppointment.PatientInfo.PatientId.Equals(_patientId)

                         select new
                         {
                             docAppointment.AppointmentDateFrom,
                             docAppointment.IsCancelled,
                             docAppointment.CancelDate,
                             docAppointment.CancelReason,
                             docAppointment.Doctor.DepartmentID,
                             docAppointment.Doctor.Department.DepartmentName,
                             docAppointment.AppointmentDateTo,
                             docAppointment.AppointmentId,
                             docAppointment.DoctorId,
                             docAppointment.Doctor.DoctorName,
                             docAppointment.PatientId,
                             PatientName = docAppointment.PatientInfo.FirstName + " " +  docAppointment.PatientInfo.MiddleName + " " + docAppointment.PatientInfo.LastName
                         }).OrderBy(x => x.AppointmentDateFrom).ToList();
            return _list;
        }
        public Dictionary<int, string> CancelAppointment(int _patientId,int _appId,string CancelReason="")
        {
            int _priorCancelTime = 0;
            Dictionary<int, string> result = new Dictionary<int, string>();
            int.TryParse(Utility.GetAppSettingKey("AppointmentCancelInAdvanceMinuts"), out _priorCancelTime);
            _db = new PatientPortalEntities();
            var app = _db.AppointmentInfoes.Where(x => x.PatientId.Equals(_patientId) && x.AppointmentId.Equals(_appId)).FirstOrDefault();
            if(app!=null)
            {
                if (app.AppointmentDateFrom >= DateTime.Now.AddMinutes(-_priorCancelTime))
                {
                    app.CancelDate = DateTime.Now;
                    app.CancelReason = CancelReason;
                    app.ModifiedBy = _patientId;
                    app.ModifiedDate = DateTime.Now;
                    app.IsCancelled = true;
                    _db.Entry(app).State = EntityState.Modified;
                    int _rowCount = _db.SaveChanges();
                    if(_rowCount>0)
                        result.Add((int)Enums.JsonResult.Success, "Appointment has been cancelled");
                    else
                        result.Add((int)Enums.JsonResult.Unsuccessful, "Appointment has not been cancelled");
                }
                else
                {
                    result.Add((int)Enums.JsonResult.Data_Expire, "You can't cancel the appointment " + _priorCancelTime + " minute(s) prior scheduled time");
                }
            }

            return result;
        }
    }
}