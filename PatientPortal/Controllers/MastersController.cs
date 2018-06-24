using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.BAL.Masters;

namespace PatientPortal.Controllers
{
    public class MastersController : CommonController
    {
        DepartmentDetails _details = null;
        // GET: Masters
        public ActionResult AddDepartments()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDepartment(string deptName)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.SaveDept(deptName)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartments()
        {
            _details = new DepartmentDetails();
            return Json(_details.DepartmentList(),JsonRequestBehavior.AllowGet);
        }
    }
}