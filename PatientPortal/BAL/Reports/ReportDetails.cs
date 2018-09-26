using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using PatientPortal.Global;

namespace PatientPortal.BAL.Reports
{
    public class ReportDetails
    {
        PatientPortalEntities _db = null;

        public List<PatientBillReport> GetBillReportData()
        {
            _db = new PatientPortalEntities();
            return _db.PatientBillReports.ToList();
        }

        public Enums.CrudStatus SetBillReportData(int PatientId, string BillNo, string BillType, DateTime BillDate, string ReportUrl, decimal BillAmount)
        {
            _db = new PatientPortalEntities();
            PatientBillReport _report = new PatientBillReport();
            _report.BillAmount = BillAmount;
            _report.BillDate = BillDate;
            _report.BillNo = BillNo;
            _report.BillType = BillType;
            _report.CreatedDate = DateTime.Now;
            _report.ModificationDate = DateTime.Now;
            _report.PatientId = PatientId;
            _db.PatientBillReports.Add(_report);
            int _result = _db.SaveChanges();
            return _result > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public List<PatientLabReport> GetLabReportData()
        {
            _db = new PatientPortalEntities();
            return _db.PatientLabReports.ToList();
        }
    }
}