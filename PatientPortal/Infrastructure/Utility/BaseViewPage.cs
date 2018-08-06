using DataLayer;
using PatientPortal.BAL.Appointments;
using PatientPortal.BAL.Masters;
using PatientPortal.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Infrastructure.Utility
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }

        public virtual HospitalDetail GetHospitalDetail()
        {
            HospitalDetails _details = new HospitalDetails();
            return _details.GetHospitalDetail();
        }
        public virtual int GetAppointmentCount()
        {
            AppointDetails _details = new AppointDetails();
            return _details.PatientAppointmentCount(User.Id);
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
        public virtual HospitalDetail GetHospitalDetail()
        {
            HospitalDetails _details = new HospitalDetails();
            return _details.GetHospitalDetail();
        }

        public virtual int GetAppointmentCount()
        {
            AppointDetails _details = new AppointDetails();
            return _details.PatientAppointmentCount(User.Id);
        }
    }
}