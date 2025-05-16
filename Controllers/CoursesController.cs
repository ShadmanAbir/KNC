using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CoursesService _service;
        public CoursesController(CoursesService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllCourses();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new CoursesViewModel());
        }

        [HttpPost]
        public ActionResult Create(CoursesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddCourse(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetCourseById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(CoursesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateCourse(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetCourseById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetCourseById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteCourse(id);
            return Json(new { success = true });
        }
    }
}