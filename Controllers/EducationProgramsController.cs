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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EducationPrograms
        public async Task<ActionResult> Index()
        {
            return View(await db.EducationPrograms.ToListAsync());
        }

        // GET: EducationPrograms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPrograms educationPrograms = await db.EducationPrograms.FindAsync(id);
            if (educationPrograms == null)
            {
                return HttpNotFound();
            }
            return View(educationPrograms);
        }

        // GET: EducationPrograms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EducationPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EducationProgramID,ProgramName,ShortName,Duration,IsDeleted,CreatedBy,CreatedDate")] EducationPrograms educationPrograms)
        {
            if (ModelState.IsValid)
            {
                db.EducationPrograms.Add(educationPrograms);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(educationPrograms);
        }

        // GET: EducationPrograms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPrograms educationPrograms = await db.EducationPrograms.FindAsync(id);
            if (educationPrograms == null)
            {
                return HttpNotFound();
            }
            return View(educationPrograms);
        }

        // POST: EducationPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EducationProgramID,ProgramName,ShortName,Duration,IsDeleted,CreatedBy,CreatedDate")] EducationPrograms educationPrograms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationPrograms).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(educationPrograms);
        }

        // GET: EducationPrograms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationPrograms educationPrograms = await db.EducationPrograms.FindAsync(id);
            if (educationPrograms == null)
            {
                return HttpNotFound();
            }
            return View(educationPrograms);
        }

        // POST: EducationPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EducationPrograms educationPrograms = await db.EducationPrograms.FindAsync(id);
            db.EducationPrograms.Remove(educationPrograms);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
