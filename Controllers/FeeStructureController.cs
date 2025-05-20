using KNC.Helper;
using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class FeeStructureController : Controller
    {
        private readonly FeeStructureService _service;
        public FeeStructureController(FeeStructureService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllFeeStructures();
            return View(items);
        }

        public ActionResult Create()
        {
            ViewBag.FrequencyList = DropDownHelper.GetFrequencyTypes();
            return PartialView("_Create", new FeeStructureViewModel());
        }

        [HttpPost]
        public ActionResult Create(FeeStructureViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddFeeStructure(vm);
                ViewBag.FrequencyList = DropDownHelper.GetFrequencyTypes(vm.Frequency);
                return Json(new { success = true });
            }
            ViewBag.FrequencyList = DropDownHelper.GetFrequencyTypes(vm.Frequency);
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetFeeStructureById(id);
            if (item == null) 
                return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(FeeStructureViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateFeeStructure(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetFeeStructureById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetFeeStructureById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteFeeStructure(id);
            return Json(new { success = true });
        }
    }
}