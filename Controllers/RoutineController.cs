using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class RoutineController : Controller
    {
        private readonly RoutineService _service;
        public RoutineController(RoutineService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllRoutines();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new RoutineViewModel());
        }

        [HttpPost]
        public ActionResult Create(RoutineViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddRoutine(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetRoutineById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(RoutineViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateRoutine(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetRoutineById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetRoutineById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteRoutine(id);
            return Json(new { success = true });
        }
    }
}