using System.Web;
using System.Web.Optimization;

namespace PatientPortal.Web
{
    /// <summary>
    /// Bundle config classs for adding script/css into bundles.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// static function to registered bundles
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Register Script

            bundles.Add(new ScriptBundle("~/bundles/LayoutBundles").Include(
                           "~/Scripts/lib/jquery-1.11.3.min.js",
                            "~/Scripts/lib/bootstrap.js",
                           "~/Scripts/lib/kendo.all.min.js",
                           "~/Scripts/lib/knockout-3.3.0.js",
                           "~/Scripts/lib/knockout.mapping-latest.debug.js",
                           "~/Scripts/lib/modernizr-custom.js",
                           "~/Scripts/lib/knockout.validation.js",
                           "~/Scripts/lib/jquery-confirm.min.js",
                           "~/Scripts/lib/jquery.mCustomScrollbar.concat.min.js",
                           "~/Scripts/lib/custom-scroll.js",
                           "~/Scripts/lib/datatables.js"));

            bundles.Add(new ScriptBundle("~/bundles/LayoutUserBundles").Include(
                         "~/Scripts/lib/jquery-1.11.3.min.js",
                         "~/Scripts/lib/bootstrap.js",
                         "~/Scripts/lib/jquery-ui.js",
                         "~/Scripts/lib/lodash.min.js",
                            "~/Scripts/lib/knockout-3.3.0.js",
                           "~/Scripts/lib/knockout.mapping-latest.debug.js",
                           "~/Scripts/lib/modernizr-custom.js",
                           "~/Scripts/lib/knockout.validation.js",
                           "~/Scripts/lib/jquery-confirm.min.js",
                           "~/Scripts/lib/jquery.mCustomScrollbar.concat.min.js",
                           "~/Scripts/lib/custom-scroll.js",
                         "~/Scripts/lib/jquery.maskedinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/LayoutLogin").Include(
                         "~/Scripts/lib/jquery-1.11.3.min.js",
                         "~/Scripts/lib/bootstrap.js",
                         "~/Scripts/lib/knockout-3.3.0.js",
                           "~/Scripts/lib/knockout.mapping-latest.debug.js",
                           "~/Scripts/lib/modernizr-custom.js",
                           "~/Scripts/lib/knockout.validation.js",
                           "~/Scripts/lib/jquery-confirm.min.js"));

            #endregion

            #region Register Styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css"));
            #endregion
        }
    }
}
