﻿using System;
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

        public List<PateintLeadger> GetBillReportData()
        {
            _db = new PatientPortalEntities();
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == WebSession.PatientId).FirstOrDefault();
            return _db.PateintLeadgers.Where(x => x.PId == patientInfo.pid).OrderByDescending(x => x.billdate).ToList();
        }

        public Enums.CrudStatus SetBillReportData(int PatientId, string BillNo, string BillType, DateTime BillDate, string ReportUrl, decimal BillAmount, string BillID)
        {
            _db = new PatientPortalEntities();
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == PatientId).FirstOrDefault();
            PateintLeadger _report = new PateintLeadger();
            _report.netamt = BillAmount;
            _report.billdate = BillDate;
            _report.billno = BillNo;
            _report.Billid = Convert.ToInt32(BillID);
            _report.vtype = BillType;
            //_report.ReportUrl = ReportUrl;
            _report.PId = Convert.ToInt32(patientInfo.pid);
            _db.PateintLeadgers.Add(_report);
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
            return _db.LabreportPdfs.Where(x => x.pid == patientInfo.pid).OrderBy(x => x.Labref).ToList();
        }

        public List<PatientLedgerModel> GetPatientLedger(DateTime? fromDate = null, DateTime? toDate = null)
        {
            _db = new PatientPortalEntities();
            DateTime _period = DateTime.Now.AddMonths(-WebSession.PatientLedgerPeriodInMonth);
            var patientInfo = _db.PatientInfoes.Where(x => x.PatientId == WebSession.PatientId).FirstOrDefault();
            List<PateintLeadger> data = new List<PateintLeadger>();
            if (fromDate == null && toDate == null)
                data = _db.PateintLeadgers.Where(x => x.PId == patientInfo.pid && DbFunctions.TruncateTime(x.billdate) >= DbFunctions.TruncateTime(_period)).OrderByDescending(x => x.billdate).ToList();
            else
            {
                data = _db.PateintLeadgers.Where(x => x.PId == patientInfo.pid && DbFunctions.TruncateTime(x.billdate) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(x.billdate) <= DbFunctions.TruncateTime(toDate)).OrderByDescending(x => x.billdate).ToList();
            }
            List<PatientLedgerModel> ledgerList = new List<PatientLedgerModel>();

            if (data != null)
            {
                foreach (var currentLedger in data)
                {
                    PatientLedgerModel newLedger = new PatientLedgerModel();
                    newLedger.Balance = currentLedger.subtotal.ToString();
                    newLedger.Date = currentLedger.billdate == null ? DateTime.Now : Convert.ToDateTime(currentLedger.billdate);
                    newLedger.Description = getBillType(currentLedger.vtype);
                    newLedger.IPNo = currentLedger.ipno;
                    newLedger.Payment = currentLedger.netamt.ToString();
                    newLedger.Receipt = currentLedger.netamt.ToString();
                    newLedger.Type = currentLedger.vtype;
                    newLedger.VNo = currentLedger.vno;
                    ledgerList.Add(newLedger);
                }
            }
            return ledgerList;
        }

        private string getBillType(string billtype)
        {
            string desc = string.Empty;
            switch (billtype)
            {
                case "SV":
                    desc = "Procedure/Diagnostic Billing";
                    break;
                case "PH":
                    desc = "Pharmacy Billing-Refund";
                    break;
                case "GP":
                    desc = "Patient Payment";
                    break;
                case "GR":
                    desc = "Receipt from Patient";
                    break;
                case "PHR":
                    desc = "Pharmacy Return";
                    break;
                case "SR":
                    desc = "Sales Return";
                    break;
            }
            return desc;
        }
    }
}