using System.Linq;
using System.Web.Mvc;
using KNC.Models;

namespace KNC.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {
            
        }

        public ActionResult Index()
        {
/*            ViewBag.StudentCount = _context.Students.Count(s => !s.IsDeleted);
            ViewBag.FacultyCount = _context.Faculties.Count(f => !f.IsDeleted);
            ViewBag.CourseCount = _context.Courses.Count(c => !c.IsDeleted);
            ViewBag.ProgramCount = _context.EducationPrograms.Count(p => !p.IsDeleted);*/
            return View();
        }
    }
}