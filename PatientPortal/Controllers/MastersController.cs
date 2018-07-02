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

        public ActionResult AddDoctors()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDepartment(string deptName)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.SaveDept(deptName)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditDepartment(string deptName,int deptId)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.EditDept(deptName, deptId)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteDepartment(int deptId)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.DeleteDept(deptId)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartments()
        {
            _details = new DepartmentDetails();
            return Json(_details.DepartmentList(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDoctor(string doctorName,int deptId)
        {
            DoctorDetails _details = new DoctorDetails();

            return Json(CrudResponse(_details.SaveDoctor(doctorName,deptId)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditDoctor(string deptName, int deptId, int docId)
        {
            DoctorDetails _details = new DoctorDetails();
            return Json(CrudResponse(_details.EditDoctor(deptName, deptId,docId)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDoctor(int docId)
        {
            DoctorDetails _details = new DoctorDetails();
            return Json(CrudResponse(_details.DeleteDoctor(docId)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDoctors()
        {
            DoctorDetails _details = new DoctorDetails();
            return Json(_details.DoctorList(), JsonRequestBehavior.AllowGet);
        }
    }
}