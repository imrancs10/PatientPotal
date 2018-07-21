using DataLayer;
using System;
using System.Collections.Generic;
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
                             docSchedule.DoctorScheduleDayID,
                             TimeFrom = docSchedule.TimeFrom + (docSchedule.TimeFromMeridiemID == 1 ? " AM" : " PM"),
                             TimeTo = docSchedule.TimeTo + (docSchedule.TimeToMeridiemID == 1 ? " AM" : " PM"),
                             docSchedule.TimeFromMeridiemID,
                             docSchedule.TimeToMeridiemID
                         }).GroupBy(x => x.DayName).ToList();
            return _list;
        }
    }
}