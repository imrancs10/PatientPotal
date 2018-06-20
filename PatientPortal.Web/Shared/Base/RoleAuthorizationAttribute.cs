using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Web
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private string _notifyUrl = string.Empty;
        public string NotifyUrl
        {
            get { return _notifyUrl; }
            set { _notifyUrl = value; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var isUserAuthenticated = ((filterContext.HttpContext.User).Identity).IsAuthenticated;
            if (isUserAuthenticated)
            {
                HttpCachePolicyBase cachePolicy =
                    filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            }

            /// This code added to support custom Unauthorized pages.
            else if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (NotifyUrl != null)
                    filterContext.Result = new RedirectResult(NotifyUrl);
                else
                    // Redirect to Login page.
                    HandleUnauthorizedRequest(filterContext);
            }
            /// End of additional code
            else
            {
                // Redirect to Login page.
                HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}