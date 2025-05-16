using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class DropDownController : Controller
    {
        private readonly DropDownService _service;
        public DropDownController(DropDownService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllDropDowns();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new DropDownViewModel());
        }

        [HttpPost]
        public ActionResult Create(DropDownViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddDropDown(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetDropDownById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(DropDownViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateDropDown(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetDropDownById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetDropDownById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteDropDown(id);
            return Json(new { success = true });
        }
    }
}