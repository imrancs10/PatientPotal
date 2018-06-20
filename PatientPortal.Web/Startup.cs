using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(PatientPortal.Web.Startup))]
namespace PatientPortal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
