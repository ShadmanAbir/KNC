using KNC.Services;
using KNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KNC.Helper;
using Newtonsoft.Json;

namespace KNC.Controllers
{
    public class FacultyController : Controller
    {
        private readonly FacultyService _facultyService;
        private readonly DesignationService _disgnationService;

        public FacultyController(FacultyService facultyService, DesignationService disgnationService)
        {
            _facultyService = facultyService;
            _disgnationService = disgnationService;
        }

        public ActionResult Index()
        {
            var cacheKey = "FacultyList";
            var cached = RedisCacheHelper.Get(cacheKey);
            List<FacultyViewModel> faculties;

            if (string.IsNullOrEmpty(cached))
            {
                faculties = _facultyService.GetAllFaculties().ToList();
                RedisCacheHelper.Set(cacheKey, JsonConvert.SerializeObject(faculties), TimeSpan.FromMinutes(10));
            }
            else
            {
                faculties = JsonConvert.DeserializeObject<List<FacultyViewModel>>(cached);
            }
            return View(faculties);
        }

        private void PopulateDesignations(FacultyViewModel model)
        {
            model.Designations = _disgnationService.GetAllDesignations()
                .Where(d => d.IsDeleted != true)
                .Select(d => new SelectListItem
                {
                    Value = d.DesignationID.ToString(),
                    Text = d.DesignationName
                }).ToList();
        }

        public ActionResult Create()
        {
            var vm = new FacultyViewModel();
            PopulateDesignations(vm);
            return PartialView("_Create", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacultyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _facultyService.AddFaculty(vm);
                RedisCacheHelper.Remove("FacultyList");
                return RedirectToAction("Index");
            }
            PopulateDesignations(vm);
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _facultyService.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            PopulateDesignations(item);
            return PartialView("_Edit", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FacultyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _facultyService.UpdateFaculty(vm);
                RedisCacheHelper.Remove("FacultyList");
                return RedirectToAction("Index");
            }
            PopulateDesignations(vm);
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _facultyService.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _facultyService.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _facultyService.DeleteFaculty(id);
            RedisCacheHelper.Remove("FacultyList");
            return RedirectToAction("Index");
        }
    }
}