﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Infrastructure.Authentication
{
    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
    }
}