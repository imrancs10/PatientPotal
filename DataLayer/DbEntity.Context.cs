﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PatientPortalEntities : DbContext
    {
        public PatientPortalEntities()
            : base("name=PatientPortalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DayMaster> DayMasters { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public virtual DbSet<Gbl_Master_Login> Gbl_Master_Login { get; set; }
        public virtual DbSet<Gbl_Master_User> Gbl_Master_User { get; set; }
        public virtual DbSet<MeridiemMaster> MeridiemMasters { get; set; }
        public virtual DbSet<PatientInfo> PatientInfoes { get; set; }
        public virtual DbSet<PatientTransaction> PatientTransactions { get; set; }
    }
}
