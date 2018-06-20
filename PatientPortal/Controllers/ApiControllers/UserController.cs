//-----------------------------------------------------------------------
// <copyright file="CustomerController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the CustomerController.cs file.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YaleNexTouch.Shared;

namespace YaleNexTouch.Web
{
    public class UserController : BaseApiController
    {

        public UserController()
        {
            
        }

        public UserController(IUserFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public UserController(IUserFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IUserFacade Facade { get; set; }


        /// <summary>
        /// This method will use to get the complete customer list.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IUserDTO Post(YaleNexTouch.DTOModel.UserDTO userData)
        {
            var customers = this.Facade.CheckAuthenticUser(userData);
            if (customers.IsValid())
            {
                return customers.Data;
            }
            else
            {
                return null;
            }
        } 
    }
}
