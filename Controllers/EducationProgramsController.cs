using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KNC.Models;

namespace KNC.Controllers
{
    public class EducationProgramsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EducationProgramsController(ApplicationDbContext db)
        {
            _db = db;
        }

        
        public async Task<ActionResult> Index()
        {
            return View(await _db.EducationPrograms.ToListAsync());
        }

        
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPrograms educationPrograms = await _db.EducationPrograms.FindAsync(id);
            if (educationPrograms == null)
            {
                return HttpNotFound();
            }
            return View(educationPrograms);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EducationProgramID,ProgramName,ShortName,Duration")] EducationPrograms educationPrograms)
        {
            educationPrograms.CreatedDate = DateTime.Now;
            educationPrograms.CreatedBy = 1;
            educationPrograms.IsDeleted = false;
            if (ModelState.IsValid)
            {
                _db.EducationPrograms.Add(educationPrograms);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(educationPrograms);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPrograms educationPrograms = await _db.EducationPrograms.FindAsync(id);
            if (educationPrograms == null)
            {
                return HttpNotFound();
            }
            return View(educationPrograms);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EducationProgramID,ProgramName,ShortName,Duration")] EducationPrograms educationPrograms)
        {
            educationPrograms.CreatedDate = DateTime.Now;
            educationPrograms.CreatedBy = 1;
            if (ModelState.IsValid)
            {
                _db.Entry(educationPrograms).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(educationPrograms);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPrograms educationPrograms = await _db.EducationPrograms.FindAsync(id);
            if (educationPrograms == null)
            {
                return HttpNotFound();
            }
            return View(educationPrograms);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EducationPrograms educationPrograms = await _db.EducationPrograms.FindAsync(id);
            _db.EducationPrograms.Remove(educationPrograms);
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
