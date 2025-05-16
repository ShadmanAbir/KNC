using KNC.Models;
using KNC.Services;
using KNC.ViewModels;
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
        public StudentController(StudentService stService)
        {
            _stService = stService;
        }

        public ActionResult Index()
        {
            var students = _stService.GetAllStudents(); 
            return View(students);
        }

        private void PopulatePrograms(StudentViewModel model)
        {
            model.Programs = _context.Programs
                .Where(p => p.IsDeleted != true)
                .Select(p => new SelectListItem
                {
                    Value = p.ProgramID.ToString(),
                    Text = p.ProgramName
                }).ToList();
        }

        public ActionResult Create()
        {

            return PartialView("_Create", new StudentsViewModel());
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
                return Json(new { success = true });
            }
            return PartialView("_Create", stVM);
        }

        public ActionResult Edit(int id)
        {
            var student = _stService.GetStudentById(id);
            if (student == null)
                return HttpNotFound();
            return PartialView("_Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(StudentsViewModel stVM)
        {
            if (ModelState.IsValid)
            {
                _stService.UpdateStudent(stVM);
                return Json(new { success = true });
            }
            return PartialView("_Edit", stVM);
        }

        public ActionResult Details(int id)
        {
            var student = _stService.GetStudentById(id);
            if (student == null)
                return HttpNotFound();
            return PartialView("_Details", student);
        }

        public ActionResult Delete(int id)
        {
            var student = _stService.GetStudentById(id);
            if (student == null)
                return HttpNotFound();
            return PartialView("_Delete", student);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _stService.DeleteStudent(id);
            return Json(new { success = true });
        }
    }
}
