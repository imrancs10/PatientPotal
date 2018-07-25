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
            //string hashPassword = Utility.GetHashString(Password);
            return _db.PatientInfoes.Where(x => x.RegistrationNumber == UserId && x.Password == Password).FirstOrDefault();
        }

        public PatientInfo GetPatientDetailByRegistrationNumber(string UserId)
        {
            _db = new PatientPortalEntities();
            return _db.PatientInfoes.Where(x => x.RegistrationNumber == UserId).FirstOrDefault();
        }


        public PatientInfo GetPatientDetailById(int Id)
        {
            _db = new PatientPortalEntities();

            return _db.PatientInfoes.Where(x => x.PatientId.Equals(Id)).FirstOrDefault();
        }
        public PatientInfo UpdatePatientDetail(PatientInfo info)
        {
            _db = new PatientPortalEntities();
            var _patientRow = _db.PatientInfoes.Where(x => x.PatientId.Equals(info.PatientId)).FirstOrDefault();
            if (_patientRow != null)
            {
                _patientRow.OTP = info.OTP;
                _patientRow.Password = !string.IsNullOrEmpty(info.Password) ? info.Password : _patientRow.Password;
                _patientRow.RegistrationNumber = !string.IsNullOrEmpty(info.RegistrationNumber) ? info.RegistrationNumber : _patientRow.RegistrationNumber; ;
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

        public Dictionary<string, object> SavePatientTransaction(PatientTransaction info)
        {
            _db = new PatientPortalEntities();
            Dictionary<string, object> result = new Dictionary<string, object>();
            int _effectRow = 0;
            var _deptRow = _db.PatientTransactions.Include(x => x.PatientInfo).Where(x => x.PatientId == info.PatientId).FirstOrDefault();
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