using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using PatientPortal.Global;
using System.Data.Entity;
using static PatientPortal.Global.Enums;

namespace PatientPortal.BAL.Patient
{
    public class PatientDetails
    {
        PatientPortalEntities _db = null;

        public PatientInfo GetPatientDetail(string UserId, string Password)
        {
            _db = new PatientPortalEntities();
            string hashPassword = Utility.GetHashString(Password);
            return _db.PatientInfoes.Where(x => (x.Email.Equals(UserId) || x.MobileNumber.Equals(UserId)) && x.Password.Equals(hashPassword)).FirstOrDefault();
        }

        public PatientInfo GetPatientDetailById(int Id)
        {
            _db = new PatientPortalEntities();

            return _db.PatientInfoes.Where(x =>x.PatientId.Equals(Id)).FirstOrDefault();
        }
        public PatientInfo UpdatePatientDetail(PatientInfo info)
        {
            _db = new PatientPortalEntities();
            var _patientRow = _db.PatientInfoes.Where(x => x.PatientId.Equals(info.PatientId)).FirstOrDefault();
            if (_patientRow != null)
            {
                _patientRow.OTP = info.OTP;
                _db.Entry(_patientRow).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return _patientRow;
        }
        public bool VerifyPatientOTP(int patientId, string OTP)
        {
            _db = new PatientPortalEntities();
            var result = _db.PatientInfoes.Where(x => x.PatientId.Equals(patientId) && x.OTP == OTP).FirstOrDefault();
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }

        public Dictionary<string, object> RegisterPatientDetail(PatientInfo info)
        {
            _db = new PatientPortalEntities();
            Dictionary<string, object> result = new Dictionary<string, object>();
            int _effectRow = 0;
            var _deptRow = _db.PatientInfoes.Where(x => x.MobileNumber.Equals(info.MobileNumber) || x.Email.Equals(info.Email)).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(info).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                result.Add("status", CrudStatus.Saved.ToString());
                result.Add("data", info);
                return result;
            }
            else
            {
                result.Add("status", CrudStatus.DataAlreadyExist.ToString());
                result.Add("data", _deptRow);
                return result;
            }
        }
    }
}