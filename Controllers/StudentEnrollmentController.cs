using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class StudentEnrollmentController : Controller
    {
        private readonly StudentEnrollmentService _service;
        public StudentEnrollmentController(StudentEnrollmentService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllEnrollments();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new StudentEnrollmentViewModel());
        }

        [HttpPost]
        public ActionResult Create(StudentEnrollmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddEnrollment(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetEnrollmentById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(StudentEnrollmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateEnrollment(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetEnrollmentById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetEnrollmentById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteEnrollment(id);
            return Json(new { success = true });
        }
    }
}