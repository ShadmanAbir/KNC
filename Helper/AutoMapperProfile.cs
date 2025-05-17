using AutoMapper;
using KNC.Models;
using KNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentsViewModel>().ReverseMap();
            CreateMap<Faculty, FacultyViewModel>().ReverseMap();
            CreateMap<EducationPrograms, EducationProgramsViewModel>().ReverseMap();
            CreateMap<ProgramCourse, ProgramCourseViewModel>().ReverseMap();
            CreateMap<Routine, RoutineViewModel>().ReverseMap();
            CreateMap<Designation, DesignationViewModel>().ReverseMap();
            CreateMap<DropDown, DropDownViewModel>().ReverseMap();
            CreateMap<EducationPrograms, EducationProgramsViewModel>().ReverseMap();
            CreateMap<StudentEnrollment, StudentEnrollmentViewModel>().ReverseMap();
            CreateMap<StudentFee, StudentFeeViewModel>().ReverseMap();
            CreateMap<MonthlyFee, MonthlyFeeViewModel>().ReverseMap();
            CreateMap<CourseTeacherAssignment, CourseTeacherAssignmentViewModel>().ReverseMap();
            CreateMap<Courses, CoursesViewModel>().ReverseMap();
        }
    }
}