using Microsoft.AspNetCore.Mvc;
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

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.EducationPrograms.Where(a => a.IsDeleted != true).ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationPrograms = await _context.EducationPrograms
                .FirstOrDefaultAsync(m => m.EducationProgramID == id && m.IsDeleted != true);
            if (educationPrograms == null)
            {
                return NotFound();
            }

            return View(educationPrograms);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EducationProgramID,ProgramName,Description,CourseType,Duration")] EducationPrograms educationPrograms)
        {
            if (ModelState.IsValid)
            {
                /*educationPrograms.CreatedBy = User.Identity.Name;*/
                educationPrograms.IsDeleted = false;
                educationPrograms.CreatedDate = DateTime.Now;
                _context.Add(educationPrograms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationPrograms);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationPrograms = await _context.EducationPrograms.SingleOrDefaultAsync( a => a.EducationProgramID == id && a.IsDeleted != true);
            if (educationPrograms == null)
            {
                return NotFound();
            }
            return View(educationPrograms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EducationProgramID,ProgramName,Description,CourseType,Duration")] EducationPrograms educationPrograms)
        {
            if (id != educationPrograms.EducationProgramID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*educationPrograms.ModifiedBy = User.Identity.Name;*/
                    educationPrograms.CreatedDate = DateTime.Now;
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

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationPrograms = await _context.EducationPrograms.SingleOrDefaultAsync(a => a.EducationProgramID == id && a.IsDeleted != true);
            if (educationPrograms != null)
            {
                educationPrograms.IsDeleted = true;
                educationPrograms.CreatedDate = DateTime.Now;
                /*educationPrograms.CreatedBy = User.Identity.Name;*/
                _context.EducationPrograms.Update(educationPrograms);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationProgramsExists(int id)
        {
            return _context.EducationPrograms.Any(e => e.EducationProgramID == id && e.IsDeleted != true);
        }
    }
}
