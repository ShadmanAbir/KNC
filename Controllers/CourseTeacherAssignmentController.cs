using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class CourseTeacherAssignmentController : Controller
    {
        private readonly CourseTeacherAssignmentService _service;
        public CourseTeacherAssignmentController(CourseTeacherAssignmentService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllAssignments();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new CourseTeacherAssignmentViewModel());
        }

        [HttpPost]
        public ActionResult Create(CourseTeacherAssignmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddAssignment(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetAssignmentById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(CourseTeacherAssignmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateAssignment(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetAssignmentById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetAssignmentById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteAssignment(id);
            return Json(new { success = true });
        }
    }
}