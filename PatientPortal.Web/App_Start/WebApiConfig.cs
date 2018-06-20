using PatientPortal.Shared.Infrastructure.Common.IoC;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PatientPortal.Web
{
    /// <summary>
    /// WebApi Config class to register routes for API Controller
    /// </summary>
    public static class WebApiConfig
    {

        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }
        /// <summary>
        /// Register webapi controller routes
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
                                        name: "Action",
                                        routeTemplate: "api/{controller}/{action}/{id}",
                                        defaults: new { id = RouteParameter.Optional }
                                      );
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
