using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using KNC.Models;

namespace KNC.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FacultiesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _db.Faculties.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = await _db.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FacultyID,FirstName,LastName,Email,Phone,PermanentAddress,CurrentAddress,Designation,JoiningDate,EducationalQualification,TeachingExperience,ClinicalExperience,LocalPublication,InternationalPublication")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _db.Faculties.Add(faculty);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(faculty);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = await _db.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FacultyID,FirstName,LastName,Email,Phone,PermanentAddress,CurrentAddress,Designation,JoiningDate,EducationalQualification,TeachingExperience,ClinicalExperience,LocalPublication,InternationalPublication")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(faculty).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(faculty);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = await _db.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Faculty faculty = await _db.Faculties.FindAsync(id);
            _db.Faculties.Remove(faculty);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
