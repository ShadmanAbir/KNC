using KNC.Helper;
using KNC.Models;
using KNC.Services;
using KNC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _stService;
        private readonly ProgramsService _programService;
        public StudentController(StudentService stService, ProgramsService programService)
        {
            _stService = stService;
            _programService = programService;
        }

        public ActionResult Index()
        {
            var cacheKey = "StudentList";
            var cached = RedisCacheHelper.Get(cacheKey);
            List<StudentsViewModel> students;

            if (string.IsNullOrEmpty(cached))
            {
                students = _stService.GetAllStudents().ToList();
                RedisCacheHelper.Set(cacheKey, JsonConvert.SerializeObject(students), TimeSpan.FromMinutes(10));
            }
            else
            {
                students = JsonConvert.DeserializeObject<List<StudentsViewModel>>(cached);
            }
            return View(students);
        }

        private void PopulatePrograms(StudentsViewModel model)
        {
            model.Programs = _programService.GetAllPrograms()
                .Where(p => p.IsDeleted != true)
                .Select(p => new SelectListItem
                {
                    Value = p.ProgramID.ToString(),
                    Text = p.ProgramName
                }).ToList();
        }

        public ActionResult Create()
        {
            var stVM = new StudentsViewModel();
            PopulatePrograms(stVM);
            return PartialView("_Create", stVM);
        }

        [HttpPost]
        public ActionResult Create(StudentsViewModel stVM)
        {
            if (stVM == null)
                return HttpNotFound();
            stVM.CreatedBy = 1;
            stVM.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _stService.AddStudent(stVM);
                RedisCacheHelper.Remove("StudentList");
                return Json(new { success = true });
            }
            PopulatePrograms(stVM);
            return PartialView("_Create", stVM);
        }

        public ActionResult Edit(int id)
        {
            var student = _stService.GetStudentById(id);
            if (student == null)
                return HttpNotFound();
            PopulatePrograms(student);
            return PartialView("_Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(StudentsViewModel stVM)
        {
            if (ModelState.IsValid)
            {
                _stService.UpdateStudent(stVM);
                RedisCacheHelper.Remove("StudentList");
                return Json(new { success = true });
            }
            PopulatePrograms(stVM);
            return PartialView("_Edit", stVM);
        }

        public ActionResult Details(int id)
        {
            var student = _stService.GetStudentById(id);
            if (student == null)
                return HttpNotFound();
            PopulatePrograms(student);
            return PartialView("_Details", student);
        }

        public ActionResult Delete(int id)
        {
            var student = _stService.GetStudentById(id);
            if (student == null)
                return HttpNotFound();
            PopulatePrograms(student);
            return PartialView("_Delete", student);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _stService.DeleteStudent(id);
            RedisCacheHelper.Remove("StudentList");
            return Json(new { success = true });
        }
    }
}
