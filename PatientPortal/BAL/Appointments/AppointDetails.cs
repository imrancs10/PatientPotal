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
        public IEnumerable<object> DeptWiseDoctorScheduleList(int deptId=0,int year=0,int month=0)
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
    }
}