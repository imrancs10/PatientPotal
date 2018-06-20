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
    /// Unity config class to register IOC container 
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Register Unity container
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Unity Config configuration
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
