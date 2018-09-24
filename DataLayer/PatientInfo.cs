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
    
    public partial class PatientInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PatientInfo()
        {
            this.AppointmentInfoes = new HashSet<AppointmentInfo>();
            this.LabReports = new HashSet<LabReport>();
            this.PatientLoginEntries = new HashSet<PatientLoginEntry>();
            this.PatientLoginHistories = new HashSet<PatientLoginHistory>();
            this.PatientTransactions = new HashSet<PatientTransaction>();
        }
    
        public int PatientId { get; set; }
        public string RegistrationNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public Nullable<int> City { get; set; }
        public string Country { get; set; }
        public Nullable<int> PinCode { get; set; }
        public string Religion { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string OTP { get; set; }
        public Nullable<int> State { get; set; }
        public byte[] Photo { get; set; }
        public string FatherOrHusbandName { get; set; }
        public string ResetCode { get; set; }
        public string CRNumber { get; set; }
        public Nullable<System.DateTime> ValidUpto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentInfo> AppointmentInfoes { get; set; }
        public virtual City City1 { get; set; }
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LabReport> LabReports { get; set; }
        public virtual PatientInfo PatientInfo1 { get; set; }
        public virtual PatientInfo PatientInfo2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientLoginEntry> PatientLoginEntries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientLoginHistory> PatientLoginHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientTransaction> PatientTransactions { get; set; }
        public virtual State State1 { get; set; }
    }
}
