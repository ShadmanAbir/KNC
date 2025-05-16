using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class FacultyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FacultyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FacultyViewModel> GetAllFaculties()
        {
            return _mapper.ProjectTo<FacultyViewModel>(_context.Faculties.Where(a => a.IsDeleted != true)).ToList();
        }

        public FacultyViewModel GetFacultyById(int id)
        {
            return _mapper.Map<FacultyViewModel>(_context.Faculties.SingleOrDefault(a => a.FacultyID == id && a.IsDeleted != true));
        }

        public void AddFaculty(FacultyViewModel vm)
        {
            _context.Faculties.Add(_mapper.Map<Faculty>(vm));
            _context.SaveChanges();
        }

        public void DeleteFaculty(int id)
        {
            var faculty = _context.Faculties.SingleOrDefault(a => a.FacultyID == id && a.IsDeleted != true);
            if (faculty != null)
            {
                faculty.IsDeleted = true;
                _context.Entry(faculty).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateFaculty(FacultyViewModel vm)
        {
            var existing = _context.Faculties.Find(vm.FacultyID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}