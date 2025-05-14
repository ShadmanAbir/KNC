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
    public class CertificatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertificatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Certificates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Certificates.ToListAsync());
        }

        // GET: Certificates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificates = await _context.Certificates
                .FirstOrDefaultAsync(m => m.CertificateID == id);
            if (certificates == null)
            {
                return NotFound();
            }

            return View(certificates);
        }

        // GET: Certificates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CertificateID,HolderID,CertificateName,HolderCode,Location,IsDeleted,CreatedBy,CreatedDate")] Certificates certificates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certificates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certificates);
        }

        // GET: Certificates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificates = await _context.Certificates.FindAsync(id);
            if (certificates == null)
            {
                return NotFound();
            }
            return View(certificates);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CertificateID,HolderID,CertificateName,HolderCode,Location,IsDeleted,CreatedBy,CreatedDate")] Certificates certificates)
        {
            if (id != certificates.CertificateID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificatesExists(certificates.CertificateID))
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
            return View(certificates);
        }

        // GET: Certificates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificates = await _context.Certificates
                .FirstOrDefaultAsync(m => m.CertificateID == id);
            if (certificates == null)
            {
                return NotFound();
            }

            return View(certificates);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificates = await _context.Certificates.FindAsync(id);
            if (certificates != null)
            {
                _context.Certificates.Remove(certificates);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificatesExists(int id)
        {
            return _context.Certificates.Any(e => e.CertificateID == id);
        }
    }
}
