using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YaleNexTouch.Web.Startup))]
namespace YaleNexTouch.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
