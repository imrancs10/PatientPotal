using Microsoft.Practices.Unity;
//-----------------------------------------------------------------------
// <copyright file="Registration.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the Registration class.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaleNexTouch.Web
{
    public class Registration
    {

        protected IUnityContainer container;

        public Registration(IUnityContainer unitycontainer)
        {
            if (unitycontainer == null)
            {
                throw new ArgumentNullException("container");
            }

            container = unitycontainer;

        }

        public IUnityContainer RegisterObjects()
        {

            container.RegisterType<YaleNexTouch.Shared.ICustomerDTO, YaleNexTouch.DTOModel.CustomerDTO>();
            container.RegisterType<YaleNexTouch.Shared.ICustomerFacade, YaleNexTouch.Facade.CustomerFacade>();
            container.RegisterType<YaleNexTouch.Shared.ICustomerBDC, YaleNexTouch.Business.CustomerBDC>();
            container.RegisterType<YaleNexTouch.Shared.ICustomerDAC, YaleNexTouch.DataAccess.CustomerDAC>();


            container.RegisterType<YaleNexTouch.Shared.IUserDTO, YaleNexTouch.DTOModel.UserDTO>();
            container.RegisterType<YaleNexTouch.Shared.IUserFacade, YaleNexTouch.Facade.UserFacade>();
            container.RegisterType<YaleNexTouch.Shared.IUserBDC, YaleNexTouch.Business.UserBDC>();
            container.RegisterType<YaleNexTouch.Shared.IUserDAC, YaleNexTouch.DataAccess.UserDAC>();


            container.RegisterType<YaleNexTouch.Entities.Entities.YaleNexTouchEntities, YaleNexTouch.Entities.Entities.YaleNexTouchEntities>();
            container.RegisterType<YaleNexTouch.Shared.IExceptionManager, YaleNexTouch.Shared.ExceptionManager>();
            container.RegisterType<YaleNexTouch.Shared.Infrastructure.Common.Logging.ILogger, YaleNexTouch.Plugin.Log4net.Log4netLogger>();

            return container;
        
        }
        
    }
}