using DataLayer;
using PatientPortal.Global;
using PatientPortal.Models.Patient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using static PatientPortal.Global.Enums;

namespace PatientPortal.BAL.Lookup
{
    public class LookupDetails
    {
        PatientPortalEntities _db = null;

        enum LookupEnum
        {
            HelpLineNo
        }
        public List<MasterLookup> GetLookupDetail()
        {
            _db = new PatientPortalEntities();
            return _db.MasterLookups.ToList();
        }
    }
}