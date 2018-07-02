using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Controllers
{
    public class HomeController : CommonController
    {
        // GET: Home
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}