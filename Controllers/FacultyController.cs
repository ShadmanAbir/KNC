using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class FacultyController : Controller
    {
        private readonly FacultyService _service;
        public FacultyController(FacultyService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllFaculties();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new FacultyViewModel());
        }

        [HttpPost]
        public ActionResult Create(FacultyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddFaculty(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(FacultyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateFaculty(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteFaculty(id);
            return Json(new { success = true });
        }
    }
}