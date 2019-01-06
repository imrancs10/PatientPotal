using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Models
{
    public class DischargeSummaryModel : PDModel
    {
        public string ipno { get; set; }
        public string iage { get; set; }
        public string idmy { get; set; }
        public string AdmitDate { get; set; }
        public string DischargeDate { get; set; }
        public string status { get; set; }
        public string admittime { get; set; }
        public string dischargedate1 { get; set; }
        public string DoctorName { get; set; }
        public string BedNo { get; set; }
        public string Matter { get; set; }
        public string CRNumber { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MobileNumber { get; set; }
    }
}