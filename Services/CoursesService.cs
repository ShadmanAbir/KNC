using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System.Collections.Generic;
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
            return _mapper.ProjectTo<CoursesViewModel>(_context.Courses.Where(a => a.IsDeleted != true)).ToList();
        }

        public CoursesViewModel GetCourseById(int id)
        {
            return _mapper.Map<CoursesViewModel>(_context.Courses.SingleOrDefault(a => a.CourseID == id && a.IsDeleted != true));
        }

        public void AddCourse(CoursesViewModel vm)
        {
            _context.Courses.Add(_mapper.Map<Courses>(vm));
            _context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var course = _context.Courses.SingleOrDefault(a => a.CourseID == id && a.IsDeleted != true);
            if (course != null)
            {
                course.IsDeleted = true;
                _context.Entry(course).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateCourse(CoursesViewModel vm)
        {
            var existing = _context.Courses.Find(vm.CourseID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}