using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataLayer;
using PatientPortal.Global;
using PatientPortal.Models.Patient;

namespace PatientPortal.BAL.Reports
{
    public class ReportDetails
    {
        PatientPortalEntities _db = null;

        public List<PatientBillReport> GetBillReportData()
        {
            _db = new PatientPortalEntities();
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == WebSession.PatientId).FirstOrDefault();
            return _db.PatientBillReports.Where(x => x.PId == patientInfo.pid).ToList();
        }

        public Enums.CrudStatus SetBillReportData(int PatientId, string BillNo, string BillType, DateTime BillDate, string ReportUrl, decimal BillAmount, string BillID)
        {
            _db = new PatientPortalEntities();
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == PatientId).FirstOrDefault();
            PatientBillReport _report = new PatientBillReport();
            _report.BillAmount = BillAmount;
            _report.BillDate = BillDate;
            _report.BillNo = BillNo;
            _report.BillId = Convert.ToInt32(BillID);
            _report.BillType = BillType;
            _report.ReportUrl = ReportUrl;
            _report.CreatedDate = DateTime.Now;
            _report.ModificationDate = DateTime.Now;
            _report.PId = Convert.ToInt32(patientInfo.pid);
            _db.PatientBillReports.Add(_report);
            int _result = _db.SaveChanges();
            return _result > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public Enums.CrudStatus SetLabReportData(int PatientId, string BillNo, string RefNo, string ReportUrl, string LabName, DateTime ReportDate, string doctorId)
        {
            _db = new PatientPortalEntities();
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == PatientId).FirstOrDefault();
            LabreportPdf _report = new LabreportPdf();
            _report.ReportDate = ReportDate;
            _report.Labref = RefNo;
            _report.BillNo = BillNo;
            _report.LabName = LabName;
            _report.CreatedDate = DateTime.Now;
            _report.Url = ReportUrl;
            _report.ModificationDate = DateTime.Now;
            _report.pid = patientInfo.pid;
            _report.DoctorId = Convert.ToInt32(doctorId);
            _db.LabreportPdfs.Add(_report);
            int _result = _db.SaveChanges();
            return _result > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public List<LabreportPdf> GetLabReportData()
        {
            _db = new PatientPortalEntities();
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == WebSession.PatientId).FirstOrDefault();
            return _db.LabreportPdfs.Where(x => x.pid == patientInfo.pid).ToList();
        }

        public List<PatientLedgerModel> GetPatientLedger()
        {
            _db = new PatientPortalEntities();
            DateTime _period = DateTime.Now.AddMonths(-WebSession.PatientLedgerPeriodInMonth);
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == WebSession.PatientId).FirstOrDefault();
            var data = _db.PateintLeadgers.Where(x => x.PId == patientInfo.pid && DbFunctions.TruncateTime(x.billdate) >= DbFunctions.TruncateTime(_period)).ToList();
            List<PatientLedgerModel> ledgerList = new List<PatientLedgerModel>();

            if (data != null)
            {
                foreach (var currentLedger in data)
                {
                    PatientLedgerModel newLedger = new PatientLedgerModel();
                    newLedger.Balance = currentLedger.subtotal.ToString();
                    newLedger.Date = currentLedger.billdate == null ? DateTime.Now : Convert.ToDateTime(currentLedger.billdate);
                    newLedger.Description = currentLedger.remarks;
                    newLedger.IPNo = currentLedger.ipno;
                    newLedger.Payment = currentLedger.netamt.ToString();
                    newLedger.Receipt = currentLedger.receiptno;
                    newLedger.Type = currentLedger.vtype;
                    newLedger.VNo = currentLedger.vno;
                    ledgerList.Add(newLedger);
                }
            }
            return ledgerList;
        }
    }
}