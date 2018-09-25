using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;

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

        public List<PatientLabReport> GetLabReportData()
        {
            _db = new PatientPortalEntities();
            return _db.PatientLabReports.ToList();
        }
    }
}