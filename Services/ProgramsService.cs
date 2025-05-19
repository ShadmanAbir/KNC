using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KNC.Services
{
    public class ProgramsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProgramsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<EducationProgramsViewModel> GetAllPrograms()
        {
            return _mapper.ProjectTo<EducationProgramsViewModel>(_context.EducationPrograms.Where(a => a.IsDeleted != true)).ToList();
        }

        public EducationProgramsViewModel GetProgramById(int id)
        {
            return _mapper.Map<EducationProgramsViewModel>(_context.EducationPrograms.SingleOrDefault(a => a.EducationProgramID == id && a.IsDeleted != true));
        }

        public void AddProgram(EducationProgramsViewModel vm)
        {
            _context.EducationPrograms.Add(_mapper.Map<EducationPrograms>(vm));
            _context.SaveChanges();
        }

        public void DeleteProgram(int id)
        {
            var program = _context.EducationPrograms.SingleOrDefault(a => a.EducationProgramID == id && a.IsDeleted != true);
            if (program != null)
            {
                program.IsDeleted = true;
                _context.Entry(program).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void UpdateProgram(EducationProgramsViewModel vm)
        {
            var existing = _context.EducationPrograms.Find(vm.EducationProgramID);
            if (existing != null)
            {
                _mapper.Map(vm, existing);
                _context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public ProgramCourseMappingViewModel ProgramCourseMapping(int programID)
        {

            var program = _context.EducationPrograms.Find(programID);
            if (program == null)
                throw new System.Exception("Program not found.");

            // All courses in the system
            var allCourses = _context.Courses.Select(c => new CoursesViewModel
            {
                CourseID = c.CourseID,
                CourseCode = c.CourseCode,
                CourseName = c.CourseName
            }).ToList();

            // Courses already mapped to this program
            var mappedCourses = _context.ProgramCourses
                .Where(pc => pc.ProgramID == programID)
                .Select(pc => new CoursesViewModel
                {
                    CourseID = pc.CourseID,
                    CourseCode = pc.Course.CourseCode,
                    CourseName = pc.Course.CourseName,
                    Year = pc.Year // Make sure your CoursesViewModel has a Year property
                }).ToList();

            // Group mapped courses by Year
            var mappedByDuration = mappedCourses
                .GroupBy(c => c.Year)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Courses NOT mapped yet
            var unmappedCourses = allCourses
                .Where(c => !mappedCourses.Any(mc => mc.CourseID == c.CourseID))
                .ToList();

            return new ProgramCourseMappingViewModel
            {
                Program = new EducationProgramsViewModel
                {
                    EducationProgramID = program.EducationProgramID,
                    ProgramName = program.ProgramName,
                    Duration = program.Duration,
                    ShortName = program.ShortName
                },
                MappedCoursesByDuration = mappedByDuration,
                UnmappedCourses = unmappedCourses
            };
        }

        public int SaveMapping(int programID, List<int> mappedCourseIds, List<int> mappedCourseDurations)
        {
            if (mappedCourseIds == null || mappedCourseDurations == null || mappedCourseIds.Count != mappedCourseDurations.Count)
            {
                throw new SystemException("Invalid Course Data");
            }

            try
            {
                // 1. Remove old mappings for this program
                var oldMappings = _context.ProgramCourses
                    .Where(pc => pc.ProgramID == programID)
                    .ToList();

                _context.ProgramCourses.RemoveRange(oldMappings);

                // 2. Add new mappings
                for (int i = 0; i < mappedCourseIds.Count; i++)
                {
                    var courseId = mappedCourseIds[i];
                    var duration = mappedCourseDurations[i];

                    var mapping = new ProgramCourse
                    {
                        ProgramID = programID,
                        CourseID = courseId,
                        Year = duration, // Update based on your property name
                        CreatedDate = DateTime.UtcNow // Optional audit info
                    };

                    _context.ProgramCourses.Add(mapping);
                }

                

                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log error (optional)
                // _logger.LogError(ex, "Failed to save mapping.");

                throw new SystemException("An error occurred while saving the mapping.", ex);
            }
        }
    }
}