using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class CourseTeacherAssignmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CourseTeacherAssignmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CourseTeacherAssignmentViewModel> GetAllAssignments()
        {
            return _mapper.ProjectTo<CourseTeacherAssignmentViewModel>(_context.CourseTeacherAssignments.Where(a => a.IsDeleted != true)).ToList();
        }

        public CourseTeacherAssignmentViewModel GetAssignmentById(int id)
        {
            return _mapper.Map<CourseTeacherAssignmentViewModel>(_context.CourseTeacherAssignments.SingleOrDefault(a => a.AssignmentID == id && a.IsDeleted != true));
        }

        public void AddAssignment(CourseTeacherAssignmentViewModel vm)
        {
            _context.CourseTeacherAssignments.Add(_mapper.Map<CourseTeacherAssignment>(vm));
            _context.SaveChanges();
        }

        public void DeleteAssignment(int id)
        {
            var assignment = _context.CourseTeacherAssignments.SingleOrDefault(a => a.AssignmentID == id && a.IsDeleted != true);
            if (assignment != null)
            {
                assignment.IsDeleted = true;
                _context.Entry(assignment).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateAssignment(CourseTeacherAssignmentViewModel vm)
        {
            var existing = _context.CourseTeacherAssignments.Find(vm.AssignmentID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}