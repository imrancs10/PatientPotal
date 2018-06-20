//-----------------------------------------------------------------------
// <copyright file="LoginController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the LoginController.cs file.</summary>
//-----------------------------------------------------------------------

namespace YaleNexTouch.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using YaleNexTouch.Business;
    using YaleNexTouch.DTOModel;
    using YaleNexTouch.Shared;

    public class AdminController : Controller
    {
 

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IUserFacade Facade { get; set; }

        // GET: /Login/
         public ActionResult Login()
        {
            UserDTO userDTO = new UserDTO();
            return View(userDTO);
        }



    }
}
