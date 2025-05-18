using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class DesignationController : Controller
    {
        private readonly DesignationService _service;
        public DesignationController(DesignationService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllDesignations();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new DesignationViewModel());
        }

        [HttpPost]
        public ActionResult Create(DesignationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddDesignation(vm);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetDesignationById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(DesignationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateDesignation(vm);
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetDesignationById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetDesignationById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteDesignation(id);
            return RedirectToAction("Index");
        }
    }
}