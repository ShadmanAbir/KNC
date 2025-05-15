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
    public class SessionsController : Controller
    {
        private ApplicationDbContext _db;

        public SessionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _db.Sessions.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessions sessions = await _db.Sessions.FindAsync(id);
            if (sessions == null)
            {
                return HttpNotFound();
            }
            return View(sessions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SessionID,SessionCode,Program,MyProperty")] Sessions sessions)
        {
            if (ModelState.IsValid)
            {
                _db.Sessions.Add(sessions);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sessions);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessions sessions = await _db.Sessions.FindAsync(id);
            if (sessions == null)
            {
                return HttpNotFound();
            }
            return View(sessions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SessionID,SessionCode,Program,MyProperty")] Sessions sessions)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(sessions).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sessions);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessions sessions = await _db.Sessions.FindAsync(id);
            if (sessions == null)
            {
                return HttpNotFound();
            }
            return View(sessions);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sessions sessions = await _db.Sessions.FindAsync(id);
            _db.Sessions.Remove(sessions);
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
