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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Certificates
        public async Task<ActionResult> Index()
        {
            return View(await db.Certificates.ToListAsync());
        }

        // GET: Certificates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = await db.Certificates.FindAsync(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            return View(certificates);
        }

        // GET: Certificates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CertificateID,HolderID,CertificateName,HolderCode,Location,IsDeleted,CreatedBy,CreatedDate")] Certificates certificates)
        {
            if (ModelState.IsValid)
            {
                db.Certificates.Add(certificates);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(certificates);
        }

        // GET: Certificates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = await db.Certificates.FindAsync(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            return View(certificates);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CertificateID,HolderID,CertificateName,HolderCode,Location,IsDeleted,CreatedBy,CreatedDate")] Certificates certificates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificates).State = EntityState.Modified;
                await db.SaveChangesAsync();
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
            Certificates certificates = await db.Certificates.FindAsync(id);
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
            Certificates certificates = await db.Certificates.FindAsync(id);
            db.Certificates.Remove(certificates);
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
