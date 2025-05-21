using KNC.Helper;
using KNC.Services;
using KNC.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class FeeStructureController : Controller
    {
        private readonly FeeStructureService _service;
        private readonly ProgramsService _programService;

        public FeeStructureController(FeeStructureService service, ProgramsService programService)
        {
            _service = service;
            _programService = programService;
        }

        public ActionResult Index()
        {
            var items = _service.GetAllFeeStructures();
            return View(items);
        }

        public ActionResult Create()
        {
            var vm = new FeeStructureViewModel
            {
                Programs = GetProgramsList()
            };
            return PartialView("_Create", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeeStructureViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.CreatedBy = User.Identity.GetUserId<int>();
                _service.AddFeeStructure(vm);
                return Json(new { success = true });
            }
            vm.Programs = GetProgramsList();
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetFeeStructureById(id);
            if (item == null) 
                return HttpNotFound();
                
            item.Programs = GetProgramsList();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FeeStructureViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.CreatedBy = User.Identity.GetUserId<int>();
                _service.UpdateFeeStructure(vm);
                return Json(new { success = true });
            }
            vm.Programs = GetProgramsList();
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

        private IEnumerable<SelectListItem> GetProgramsList()
        {
            return _programService.GetAllPrograms()
                .Where(p => !p.IsDeleted)
                .Select(p => new SelectListItem
                {
                    Value = p.EducationProgramID.ToString(),
                    Text = p.ProgramName
                });
        }
    }
}