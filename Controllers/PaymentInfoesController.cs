using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using KNC.Models;

namespace KNC.Controllers
{
    public class PaymentInfoesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentInfoesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _db.PaymentInfos.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = await _db.PaymentInfos.FindAsync(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PaymentID,StudentID,Amount,PaymentDate")] PaymentInfo paymentInfo)
        {
            if (ModelState.IsValid)
            {
                _db.PaymentInfos.Add(paymentInfo);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(paymentInfo);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = await _db.PaymentInfos.FindAsync(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PaymentID,StudentID,Amount,PaymentDate")] PaymentInfo paymentInfo)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(paymentInfo).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentInfo);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = await _db.PaymentInfos.FindAsync(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PaymentInfo paymentInfo = await _db.PaymentInfos.FindAsync(id);
            _db.PaymentInfos.Remove(paymentInfo);
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
