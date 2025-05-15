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
    public class PaymentInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentInfoes
        public async Task<ActionResult> Index()
        {
            return View(await db.PaymentInfos.ToListAsync());
        }

        // GET: PaymentInfoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = await db.PaymentInfos.FindAsync(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        // GET: PaymentInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PaymentID,StudentID,Amount,PaymentDate,IsDeleted,CreatedBy,CreatedDate")] PaymentInfo paymentInfo)
        {
            if (ModelState.IsValid)
            {
                db.PaymentInfos.Add(paymentInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(paymentInfo);
        }

        // GET: PaymentInfoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = await db.PaymentInfos.FindAsync(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        // POST: PaymentInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PaymentID,StudentID,Amount,PaymentDate,IsDeleted,CreatedBy,CreatedDate")] PaymentInfo paymentInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentInfo);
        }

        // GET: PaymentInfoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = await db.PaymentInfos.FindAsync(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        // POST: PaymentInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PaymentInfo paymentInfo = await db.PaymentInfos.FindAsync(id);
            db.PaymentInfos.Remove(paymentInfo);
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
