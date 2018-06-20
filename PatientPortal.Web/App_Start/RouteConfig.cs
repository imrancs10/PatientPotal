using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PatientPortal.Web
{
    /// <summary>
    /// Route config class to registers routes
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// static Register routes function
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "PdfFooter",
            url: "SurveyUser/PdfFooter",
            defaults: new { controller = "SurveyUser", action = "PdfFooter" }
            );

            //routes.MapRoute(
            //name: "DownloadPDF",
            //url: "SurveyUser/DownloadSurveyResult/",
            //defaults: new { controller = "SurveyUser", action = "DownloadSurveyResult", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
            name: "DownloadPDFAndAttachedPDF",
            url: "SurveyUser/DownloadPDFAndAttachedPDF/{file}",
            defaults: new { controller = "SurveyUser", action = "DownloadPDFAndAttachedPDF", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DownloadUserSurveyResult",
            url: "SurveyUser/DownloadUserSurveyResult/{file}",
            defaults: new { controller = "SurveyUser", action = "DownloadUserSurveyResult", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "PDFAttached",
            url: "SurveyUser/PDFAttached/{file}",
            defaults: new { controller = "SurveyUser", action = "PDFAttached", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DownloadPDFPastSurvey",
            url: "SurveyUser/DownloadPDFPastSurvey",
            defaults: new { controller = "SurveyUser", action = "DownloadPDFPastSurvey", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DownloadPDF",
            url: "SurveyUser/DownloadPDF/{file}",
            defaults: new { controller = "SurveyUser", action = "DownloadPDF", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "SignOut",
            url: "signout/",
            defaults: new { controller = "Admin", action = "SignOut" }
            );

            routes.MapRoute(
            name: "PastSurvey",
            url: "pastsurvey/",
            defaults: new { controller = "SurveyUser", action = "PastSurvey", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "AdditionalInformation",
            url: "additionalInformation/",
            defaults: new { controller = "SurveyUser", action = "AdditionalInformation", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "UserRecommendation",
            url: "recommendation/",
            defaults: new { controller = "SurveyUser", action = "Recommendation", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "UserRecommendationPage",
            url: "recommendation/",
            defaults: new { controller = "SurveyUser", action = "RecommendationPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "CreatePDF",
                url: "SurveyUser/CreatePDF",
                defaults: new { controller = "SurveyUser", action = "CreatePDF", id = UrlParameter.Optional }
             );


            routes.MapRoute(
            name: "UserSurvey",
            url: "surveyuser/{surveyId}",
            defaults: new { controller = "SurveyUser", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "ConfirmPassword",
            url: "confirmpassword/{emailId}",
            defaults: new { controller = "SurveyUser", action = "ConfirmPassword", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Product",
            url: "product/",
            defaults: new { controller = "Admin", action = "Product" }
            );

            routes.MapRoute(
            name: "Survey",
            url: "survey/",
            defaults: new { controller = "Admin", action = "Survey" }
            );

            routes.MapRoute(
               name: "Question",
               url: "question/",
               defaults: new { controller = "Admin", action = "Question" }

            );

            routes.MapRoute(
               name: "Login",
               url: "login/",
               defaults: new { controller = "Admin", action = "Login", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Admin", action = "Login", id = UrlParameter.Optional }
             );

            routes.MapRoute(
             name: "Default11",
             url: "SurveyUser/CreatePDF",
             defaults: new { controller = "SurveyUser", action = "CreatePDF", id = UrlParameter.Optional }
         );


        }
    }
}
