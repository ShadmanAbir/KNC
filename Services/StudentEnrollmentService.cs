using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class StudentEnrollmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentEnrollmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<StudentEnrollmentViewModel> GetAllEnrollments()
        {
            return _mapper.ProjectTo<StudentEnrollmentViewModel>(_context.StudentEnrollments.Where(a => a.IsDeleted != true)).ToList();
        }

        public StudentEnrollmentViewModel GetEnrollmentById(int id)
        {
            return _mapper.Map<StudentEnrollmentViewModel>(_context.StudentEnrollments.SingleOrDefault(a => a.StudentID == id && a.IsDeleted != true));
        }

        public void AddEnrollment(StudentEnrollmentViewModel vm)
        {
            _context.StudentEnrollments.Add(_mapper.Map<StudentEnrollment>(vm));
            _context.SaveChanges();
        }

        public void DeleteEnrollment(int id)
        {
            var enrollment = _context.StudentEnrollments.SingleOrDefault(a => a.StudentID == id && a.IsDeleted != true);
            if (enrollment != null)
            {
                enrollment.IsDeleted = true;
                _context.Entry(enrollment).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateEnrollment(StudentEnrollmentViewModel vm)
        {
            var existing = _context.StudentEnrollments.Find(vm.StudentID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}