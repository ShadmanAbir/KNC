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
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _db.Students.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Create()
        {
            ViewBag.ProgramList = new SelectList(_db.EducationPrograms, "EducationProgramID", "ProgramName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentID,StudentCode,FirstName,LastName,Email,Phone,PermanentAddress,CurrentAddress,AdmissionDate,Program")] Student student)
        {

            ViewBag.ProgramList = new SelectList(_db.EducationPrograms, "EducationProgramID", "ProgramName");
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.ProgramList = new SelectList(_db.EducationPrograms, "EducationProgramID", "ProgramName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentID,StudentCode,FirstName,LastName,Email,Phone,PermanentAddress,CurrentAddress,AdmissionDate,Program")] Student student)
        {
            ViewBag.ProgramList = new SelectList(_db.EducationPrograms, "EducationProgramID", "ProgramName");
            if (ModelState.IsValid)
            {
                _db.Entry(student).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await _db.Students.FindAsync(id);
            _db.Students.Remove(student);
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
