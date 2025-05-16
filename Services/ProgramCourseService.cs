using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class ProgramCourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProgramCourseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProgramCourseViewModel> GetAllProgramCourses()
        {
            return _mapper.ProjectTo<ProgramCourseViewModel>(_context.ProgramCourses.Where(a => a.IsDeleted != true)).ToList();
        }

        public ProgramCourseViewModel GetProgramCourseById(int id)
        {
            return _mapper.Map<ProgramCourseViewModel>(_context.ProgramCourses.SingleOrDefault(a => a.ProgramCourseID == id && a.IsDeleted != true));
        }

        public void AddProgramCourse(ProgramCourseViewModel vm)
        {
            _context.ProgramCourses.Add(_mapper.Map<ProgramCourse>(vm));
            _context.SaveChanges();
        }

        public void DeleteProgramCourse(int id)
        {
            var pc = _context.ProgramCourses.SingleOrDefault(a => a.ProgramCourseID == id && a.IsDeleted != true);
            if (pc != null)
            {
                pc.IsDeleted = true;
                _context.Entry(pc).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateProgramCourse(ProgramCourseViewModel vm)
        {
            var existing = _context.ProgramCourses.Find(vm.ProgramCourseID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}