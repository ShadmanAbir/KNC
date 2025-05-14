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
    public class EducationProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EducationPrograms
        public async Task<IActionResult> Index()
        {
            return View(await _context.EducationPrograms.ToListAsync());
        }

        // GET: EducationPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationPrograms = await _context.EducationPrograms
                .FirstOrDefaultAsync(m => m.EducationProgramID == id);
            if (educationPrograms == null)
            {
                return NotFound();
            }

            return View(educationPrograms);
        }

        // GET: EducationPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EducationProgramID,ProgramName,Description,CourseType,Duration,IsDeleted,CreatedBy,CreatedDate")] EducationPrograms educationPrograms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationPrograms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationPrograms);
        }

        // GET: EducationPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationPrograms = await _context.EducationPrograms.FindAsync(id);
            if (educationPrograms == null)
            {
                return NotFound();
            }
            return View(educationPrograms);
        }

        // POST: EducationPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EducationProgramID,ProgramName,Description,CourseType,Duration,IsDeleted,CreatedBy,CreatedDate")] EducationPrograms educationPrograms)
        {
            if (id != educationPrograms.EducationProgramID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationPrograms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationProgramsExists(educationPrograms.EducationProgramID))
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
            return View(educationPrograms);
        }

        // GET: EducationPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationPrograms = await _context.EducationPrograms
                .FirstOrDefaultAsync(m => m.EducationProgramID == id);
            if (educationPrograms == null)
            {
                return NotFound();
            }

            return View(educationPrograms);
        }

        // POST: EducationPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationPrograms = await _context.EducationPrograms.FindAsync(id);
            if (educationPrograms != null)
            {
                _context.EducationPrograms.Remove(educationPrograms);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationProgramsExists(int id)
        {
            return _context.EducationPrograms.Any(e => e.EducationProgramID == id);
        }
    }
}
