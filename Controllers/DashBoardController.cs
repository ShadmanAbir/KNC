using System.Linq;
using System.Web.Mvc;
using KNC.Models;

namespace KNC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            ViewBag.StudentCount = _context.Students.Count(s => !s.IsDeleted);
            ViewBag.FacultyCount = _context.Faculties.Count(f => !f.IsDeleted);
            ViewBag.CourseCount = _context.Courses.Count(c => !c.IsDeleted);
            ViewBag.ProgramCount = _context.Programs.Count(p => !p.IsDeleted);
            return View();
        }
    }
}