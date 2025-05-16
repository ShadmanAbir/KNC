using KNC.Models;
using KNC.Services;
using KNC.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace KNC.Controllers
{
    public class FacultyController : Controller
    {
        private readonly FacultyService _facultyService;
        private readonly ApplicationDbContext _context;

        public FacultyController(FacultyService facultyService, ApplicationDbContext context)
        {
            _facultyService = facultyService;
            _context = context;
        }

        public ActionResult Index()
        {
            var faculties = _facultyService.GetAll();
            return View(faculties);
        }

        private void PopulateDesignations(FacultyViewModel model)
        {
            model.Designations = _service.GetAllDesignations()
                .Where(d => d.IsDeleted != true)
                .Select(d => new SelectListItem
                {
                    Value = d.DesignationID.ToString(),
                    Text = d.Title
                }).ToList();
        }

        public ActionResult Create()
        {
            return PartialView("_Create", new FacultyViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacultyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.AddFaculty(vm);
                return Json(new { success = true });
            }
            return PartialView("_Create", vm);
        }

        public ActionResult Edit(int id)
        {
            var item = _service.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Edit", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FacultyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateFaculty(vm);
                return Json(new { success = true });
            }
            return PartialView("_Edit", vm);
        }

        public ActionResult Details(int id)
        {
            var item = _service.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Details", item);
        }

        public ActionResult Delete(int id)
        {
            var item = _service.GetFacultyById(id);
            if (item == null) return HttpNotFound();
            return PartialView("_Delete", item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteFaculty(id);
            return Json(new { success = true });
        }
    }
}