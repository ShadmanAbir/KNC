using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KNC.Data;
using KNC.Models;

namespace KNC.Controllers
{
    public class PaymentInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentInfo.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (paymentInfo == null)
            {
                return NotFound();
            }

            return View(paymentInfo);
        }

        // GET: PaymentInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,StudentID,Amount,PaymentDate,IsDeleted,CreatedBy,CreatedDate")] PaymentInfo paymentInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentInfo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo.FindAsync(id);
            if (paymentInfo == null)
            {
                return NotFound();
            }
            return View(paymentInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,StudentID,Amount,PaymentDate,IsDeleted,CreatedBy,CreatedDate")] PaymentInfo paymentInfo)
        {
            if (id != paymentInfo.PaymentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentInfoExists(paymentInfo.PaymentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paymentInfo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInfo = await _context.PaymentInfo
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (paymentInfo == null)
            {
                return NotFound();
            }

            return View(paymentInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentInfo = await _context.PaymentInfo.FindAsync(id);
            if (paymentInfo != null)
            {
                _context.PaymentInfo.Remove(paymentInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentInfoExists(int id)
        {
            return _context.PaymentInfo.Any(e => e.PaymentID == id);
        }
    }
}
