using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace PatientPortal.Web
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action for check session
        /// </summary>
        /// <param name="filterContext">Action executing context object</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName == "Index" || filterContext.ActionDescriptor.ActionName == "Recommendation" || filterContext.ActionDescriptor.ActionName == "AdditionalInformation")
            {
                if (System.Web.HttpContext.Current.Session["RecommendedGuid"] == null)
                {
                    filterContext.Result = new RedirectResult("~/login/");
                }
                return;
            }

            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectResult("~/login/");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}