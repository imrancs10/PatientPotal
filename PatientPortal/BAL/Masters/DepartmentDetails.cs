using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;
using PatientPortal.Global;
using PatientPortal.Models.Masters;

namespace PatientPortal.BAL.Masters
{
    public class DepartmentDetails
    {
        PatientPortalEntities _db = null;

        public Enums.CrudStatus SaveDept(string deptName)
        {
            _db = new PatientPortalEntities();
            int _effectRow = 0;
            var _deptRow = _db.Departments.Where(x => x.DepartmentName.Equals(deptName)).FirstOrDefault();
            if (_deptRow == null)
            {
                Department _newDept = new Department();
                _newDept.DepartmentName = deptName;
                _db.Entry(_newDept).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }

        public List<DepartmentModel> DepartmentList()
        {
            _db = new PatientPortalEntities();
            var _list = (from dept in _db.Departments
                         select new DepartmentModel
                         {
                             DeparmentName = dept.DepartmentName,
                             DepartmentId = dept.DepartmentID
                         }).ToList();
            return _list != null ? _list : new List<DepartmentModel>();
        }
    }
}