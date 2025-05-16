using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class EducationProgramsController : Controller
    {
        private readonly EducationProgramsService _service;
        public EducationProgramsController(EducationProgramsService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllPrograms();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new EducationProgramsViewModel());
        }

        [HttpPost]
        public ActionResult Create(EducationProgramsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddProgram(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetProgramById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(EducationProgramsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateProgram(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetProgramById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetProgramById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteProgram(id);
            return Json(new { success = true });
        }
    }
}