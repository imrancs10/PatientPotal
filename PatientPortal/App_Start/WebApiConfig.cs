using YaleNexTouch.Shared.Infrastructure.Common.IoC;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace YaleNexTouch.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Add the unity container.
            var container = new UnityContainer();

            var registration = new Registration(container);
            var registrationContainer = registration.RegisterObjects();

            config.DependencyResolver = new UnityResolver(registrationContainer);

            ContainerProvider.InitializeContainer(registrationContainer);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}
