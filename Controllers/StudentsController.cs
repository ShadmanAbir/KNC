using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KNC.Data;
using KNC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using KNC.Service;

namespace KNC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HelperService _helper;

        public StudentsController(ApplicationDbContext context,HelperService helper)
        {
            _context = context;
            _helper = helper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await (from a in _context.Student
                        join b in _context.EducationPrograms on a.Program equals b.EducationProgramID into temp
                        from b in temp.DefaultIfEmpty()
                        where a.IsDeleted != true
                        select new Student
                        {
                            StudentID = a.StudentID,
                            Name = a.FirstName + " " + a.LastName,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Email = a.Email,
                            Phone = a.Phone,
                            PermanentAddress = a.PermanentAddress,
                            CurrentAddress = a.CurrentAddress,
                            AdmissionDate = a.AdmissionDate,
                            ProgramName = b.ProgramName
                        }).ToListAsync();

            return View(data);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FirstOrDefaultAsync(m => m.StudentID == id && m.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        public IActionResult Create()
        {
            ViewBag.ProgramList = new SelectList(_context.EducationPrograms.Where(a => a.IsDeleted != true).ToList(), "EducationProgramID", "ProgramName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,StudentCode,FirstName,LastName,Email,Phone,PermanentAddress,CurrentAddress,AdmissionDate,Program")] Student student)
        {
            ViewBag.ProgramList = new SelectList(_context.EducationPrograms.Where(a => a.IsDeleted != true).ToList(), "EducationProgramID", "ProgramName");
            var count = await _context.Student.CountAsync();
            var program = await _context.EducationPrograms.FirstOrDefaultAsync(a => a.EducationProgramID == student.Program && a.IsDeleted != true);
            if (program == null) {
                ModelState.AddModelError("Program", "Program not found.");
                return View(student);
            }
            student.StudentCode = _helper.GenerateStudentCode(count +1, program.ProgramName, count);
            if (ModelState.IsValid)
            {
                /*student.CreatedBy = User.Identity.Name;*/
                student.CreatedDate = DateTime.Now;
                student.IsDeleted = false;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.SingleOrDefaultAsync(a => a.StudentID == id && a.IsDeleted != true);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,StudentCode,FirstName,LastName,Email,Phone,PermanentAddress,CurrentAddress,AdmissionDate,Program")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*student.CreatedBy = User.Identity.Name;*/
                    student.CreatedDate = DateTime.Now;
                    student.IsDeleted = false;
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.SingleOrDefaultAsync(m => m.StudentID == id && m.IsDeleted != true);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.SingleOrDefaultAsync(a => a.StudentID == id && a.IsDeleted != true);
            if (student != null)
            {
                /*student.CreatedBy = User.Identity.Name;*/
                student.IsDeleted = true;
                student.CreatedDate = DateTime.Now;
                _context.Student.Update(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentID == id && e.IsDeleted != true);
        }
    }
}
