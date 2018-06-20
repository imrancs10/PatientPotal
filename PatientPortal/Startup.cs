using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PatientPortal.Startup))]
namespace PatientPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
