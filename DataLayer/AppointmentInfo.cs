//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppointmentInfo
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public System.DateTime AppointmentDateFrom { get; set; }
        public System.DateTime AppointmentDateTo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedBy { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual PatientInfo PatientInfo { get; set; }
    }
}
