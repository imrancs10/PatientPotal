using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;
using PatientPortal.Global;
using PatientPortal.Models.Masters;
using PatientPortal.Infrastructure;
using PatientPortal.Infrastructure.Utility;

namespace PatientPortal.BAL.Masters
{
    public class DoctorDetails
    {
        PatientPortalEntities _db = null;
        public Enums.CrudStatus SaveDoctor(string doctorName,int deptId)
        {
            _db = new PatientPortalEntities();
            int _effectRow = 0;
            var _deptRow = _db.Doctors.Where(x => x.DoctorName.Equals(doctorName) && x.DepartmentID.Equals(deptId)).FirstOrDefault();
            if (_deptRow == null)
            {
                Doctor _newDoc = new Doctor();
                _newDoc.DoctorName = doctorName;
                _newDoc.DepartmentID = deptId;
                _db.Entry(_newDoc).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
        public Enums.CrudStatus EditDoctor(string doctorName, int deptId,int docId)
        {
            _db = new PatientPortalEntities();
            int _effectRow = 0;
            var _docRow = _db.Doctors.Where(x => x.DoctorID.Equals(docId)).FirstOrDefault();
            if (_docRow != null)
            {
                _docRow.DoctorName = doctorName;
                _docRow.DepartmentID = deptId;
                _db.Entry(_docRow).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
            }
            else
                return Enums.CrudStatus.DataNotFound;
        }
        public Enums.CrudStatus DeleteDoctor(int docId)
        {
            _db = new PatientPortalEntities();
            int _effectRow = 0;
            var _docRow = _db.Doctors.Where(x => x.DoctorID.Equals(docId)).FirstOrDefault();
            if (_docRow != null)
            {
                _db.Doctors.Remove(_docRow);
                //_db.Entry(_deptRow).State = EntityState.Deleted;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Deleted : Enums.CrudStatus.NotDeleted;
            }
            else
                return Enums.CrudStatus.DataNotFound;
        }
        public List<DoctorModel> DoctorList(int deptId=0)
        {
            _db = new PatientPortalEntities();
            var _list = (from doc in _db.Doctors
                         from dept in _db.Departments.Where(x=>x.DepartmentID.Equals(doc.DepartmentID))
                         orderby dept.DepartmentName
                         where deptId==0 || deptId.Equals(doc.DepartmentID)
                         select new DoctorModel
                         {
                             DoctorName = doc.DoctorName,
                             DepartmentId = dept.DepartmentID,
                             DoctorId=doc.DoctorID,
                             DepartmentName=dept.DepartmentName
                         }).ToList();
            return _list != null ? _list : new List<DoctorModel>();
        }
        public IEnumerable<object> GetDoctorLeaveList(int doctorId)
        {
            _db = new PatientPortalEntities();
            return (from leave in _db.DoctorLeaves.Where(x => x.DoctorId.Equals(doctorId))
                           select new
                           {
                               leave.DoctorId,
                               leave.Doctor.DoctorName,
                               leave.Doctor.DepartmentID,
                               leave.Doctor.Department.DepartmentName,
                               leave.LeaveDate
                           }).OrderBy(x => x.LeaveDate).ThenBy(x => x.DoctorName).ToList();
            
        }
        public Enums.CrudStatus SaveDoctorLeave(int doctorId, DateTime leaveDate)
        {
            if (doctorId < 1)
            {
                return Enums.CrudStatus.InvalidPostedData;
            }
            else if (leaveDate.Date < DateTime.Now.Date)
            {
                return Enums.CrudStatus.InvalidPastDate;
            }
            else
            {
                _db = new PatientPortalEntities();
                int _effectRow = 0;
                var _deptRow = _db.DoctorLeaves.Where(x => x.DoctorId.Equals(doctorId) && x.LeaveDate.Equals(leaveDate)).FirstOrDefault();
                if (_deptRow == null)
                {
                    DoctorLeave _newDoc = new DoctorLeave();
                    _newDoc.DoctorId = doctorId;
                    _newDoc.LeaveDate = leaveDate;
                    _newDoc.CreatedDate = DateTime.Now;
                    _db.Entry(_newDoc).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    if (_effectRow > 0)
                    {
                        var appointments = _db.AppointmentInfoes.Where(x => 
                                                                            x.DoctorId.Equals(doctorId) && 
                                                                            !x.IsCancelled && 
                                                                            DbFunctions.TruncateTime(x.AppointmentDateFrom)==DbFunctions.TruncateTime(leaveDate)
                                                                      ).ToList();
                        if(appointments.Count>0)
                        {
                            foreach (AppointmentInfo appointment in appointments)
                            {
                                Message msg = new Message()
                                {
                                    MessageTo = appointment.PatientInfo.Email,
                                    MessageNameTo = appointment.PatientInfo.FirstName + " " + appointment.PatientInfo.MiddleName + (string.IsNullOrWhiteSpace(appointment.PatientInfo.MiddleName) ? string.Empty : " ") + appointment.PatientInfo.LastName,
                                    Subject = "Appointment Booking Confirmation",
                                    Body = EmailHelper.GetDoctorAbsentEmail(appointment.PatientInfo.FirstName, appointment.PatientInfo.MiddleName, appointment.PatientInfo.LastName, appointment.Doctor.DoctorName, leaveDate, appointment.Doctor.Department.DepartmentName)
                                };
                                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                                sendMessageStrategy.SendMessages();
                            }
                        }
                        return Enums.CrudStatus.Saved;
                    }
                    else
                    {
                        return Enums.CrudStatus.NotSaved;
                    }
                }
                else
                    return Enums.CrudStatus.DataAlreadyExist;
            }
        }
    }
}