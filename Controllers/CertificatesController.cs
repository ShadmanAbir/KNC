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
    public class CertificatesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CertificatesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _db.Certificates.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = await _db.Certificates.FindAsync(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            return View(certificates);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CertificateID,HolderID,CertificateName,HolderCode,Location")] Certificates certificates)
        {
            if (ModelState.IsValid)
            {
                _db.Certificates.Add(certificates);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(certificates);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = await _db.Certificates.FindAsync(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            return View(certificates);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CertificateID,HolderID,CertificateName,HolderCode,Location")] Certificates certificates)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(certificates).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(certificates);
        }

        // GET: Certificates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = await _db.Certificates.FindAsync(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            return View(certificates);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Certificates certificates = await _db.Certificates.FindAsync(id);
            _db.Certificates.Remove(certificates);
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
