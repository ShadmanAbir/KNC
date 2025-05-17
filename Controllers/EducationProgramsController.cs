using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class EducationProgramsController : Controller
    {
        private readonly ProgramsService _service;
        public EducationProgramsController(ProgramsService service)
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
            vm.CreatedBy = 1;
            vm.CreatedDate = DateTime.Now;
            vm.IsDeleted = false;
            if (ModelState.IsValid)
            {
                _service.AddProgram(vm);
                return View("Index");
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
            vm.CreatedBy = 1;
            vm.CreatedDate = DateTime.Now;
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