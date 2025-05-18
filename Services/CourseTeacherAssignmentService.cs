using AutoMapper;
using AutoMapper.QueryableExtensions;
using KNC.Models;
using KNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return _context.CourseTeacherAssignments
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ProjectTo<CourseTeacherAssignmentViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public CourseTeacherAssignmentViewModel GetAssignmentById(int id)
        {
            var assignment = _context.CourseTeacherAssignments
                .AsNoTracking()
                .SingleOrDefault(a => a.AssignmentID == id && !a.IsDeleted);

            return assignment == null ? null : _mapper.Map<CourseTeacherAssignmentViewModel>(assignment);
        }

        public void AddAssignment(CourseTeacherAssignmentViewModel vm)
        {
            var entity = _mapper.Map<CourseTeacherAssignment>(vm);

            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = vm.CreatedBy;
            entity.IsDeleted = false;

            _context.CourseTeacherAssignments.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteAssignment(int id)
        {
            var assignment = _context.CourseTeacherAssignments
                .SingleOrDefault(a => a.AssignmentID == id && !a.IsDeleted);

            if (assignment != null)
            {
                assignment.IsDeleted = true;
                assignment.CreatedDate = DateTime.Now; // Track modification time
                _context.Entry(assignment).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateAssignment(CourseTeacherAssignmentViewModel vm)
        {
            var existing = _context.CourseTeacherAssignments.Find(vm.AssignmentID);
            if (existing != null)
            {
                bool wasDeleted = existing.IsDeleted;

                _mapper.Map(vm, existing);

                existing.IsDeleted = wasDeleted;
                existing.CreatedDate = DateTime.Now;
                existing.CreatedBy = vm.CreatedBy;

                _context.Entry(existing).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}