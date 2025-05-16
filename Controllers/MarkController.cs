using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class MarkController : Controller
    {
        private readonly MarkService _service;
        public MarkController(MarkService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllMarks();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new MarkViewModel());
        }

        [HttpPost]
        public ActionResult Create(MarkViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddMark(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetMarkById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(MarkViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateMark(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetMarkById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetMarkById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteMark(id);
            return Json(new { success = true });
        }
    }
}