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
    
    public partial class AppointmentSetting
    {
        public int Id { get; set; }
        public int AppointmentSlot { get; set; }
        public int CalenderPeriod { get; set; }
        public string AppointmentMessage { get; set; }
        public int AppointmentLimitPerUser { get; set; }
        public int AppointmentCancelPeriod { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string AutoCancelMessage { get; set; }
        public bool IsActiveAppointmentMessage { get; set; }
    }
}