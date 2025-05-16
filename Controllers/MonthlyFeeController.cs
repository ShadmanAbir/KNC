using KNC.Services;
using KNC.ViewModels;
using System;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class MonthlyFeeController : Controller
    {
        private readonly MonthlyFeeService _service;
        public MonthlyFeeController(MonthlyFeeService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllMonthlyFees();
            return View(items);
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new MonthlyFeeViewModel());
        }

        [HttpPost]
        public ActionResult Create(MonthlyFeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddMonthlyFee(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetMonthlyFeeById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(MonthlyFeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateMonthlyFee(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetMonthlyFeeById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetMonthlyFeeById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteMonthlyFee(id);
            return Json(new { success = true });
        }
    }
}