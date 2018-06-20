using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Web
{
    /// <summary>
    /// filter config class to register global filters
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register Global filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
