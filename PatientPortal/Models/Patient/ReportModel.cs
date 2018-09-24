using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Models.Patient
{
    public class ReportModel
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime Date { get; set; }
        public string ReportName { get; set; }
    }
}