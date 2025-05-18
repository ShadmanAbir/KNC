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
    public class CoursesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CoursesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CoursesViewModel> GetAllCourses()
        {
            return _context.Courses
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .ProjectTo<CoursesViewModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public CoursesViewModel GetCourseById(int id)
        {
            var course = _context.Courses
                .AsNoTracking()
                .SingleOrDefault(a => a.CourseID == id && !a.IsDeleted);
            return course == null ? null : _mapper.Map<CoursesViewModel>(course);
        }

        public void AddCourse(CoursesViewModel vm)
        {
            var entity = _mapper.Map<Courses>(vm);
            
            // Set initial values for new course
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = vm.CreatedBy;
            entity.IsDeleted = false;
            
            _context.Courses.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var course = _context.Courses
                .SingleOrDefault(a => a.CourseID == id && !a.IsDeleted);
            
            if (course != null)
            {
                course.IsDeleted = true;
                course.CreatedDate = DateTime.Now; // Update modification time
                _context.Entry(course).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateCourse(CoursesViewModel vm)
        {
            var existing = _context.Courses.Find(vm.CourseID);
            if (existing != null)
            {
                // Keep track of deletion status
                bool wasDeleted = existing.IsDeleted;
                
                // Map all properties
                _mapper.Map(vm, existing);
                
                // Preserve/update tracking fields
                existing.IsDeleted = wasDeleted; // Keep original deletion status
                existing.CreatedDate = DateTime.Now;
                existing.CreatedBy = vm.CreatedBy;
                
                _context.Entry(existing).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}