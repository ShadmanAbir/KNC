using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class ProgramCourseController : Controller
    {
        private readonly ProgramCourseService _service;
        public ProgramCourseController(ProgramCourseService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllProgramCourses();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new ProgramCourseViewModel());
        }

        [HttpPost]
        public ActionResult Create(ProgramCourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddProgramCourse(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetProgramCourseById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(ProgramCourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateProgramCourse(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetProgramCourseById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetProgramCourseById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteProgramCourse(id);
            return Json(new { success = true });
        }
    }
}