using KNC.Models;
using System;
using System.Collections.Generic;

namespace KNC.ViewModels
{
    public class EducationProgramsViewModel : BaseViewModel
    {
        public int EducationProgramID { get; set; }
        public string ProgramName { get; set; }
        public string ShortName { get; set; }
        public int Duration { get; set; }

        public List<CoursesViewModel> AllCourses { get; set; }
        public List<ProgramCourseViewModel> ProgramCourses { get; set; } 

    }

    public class ProgramCourseMappingViewModel
    {
        public EducationProgramsViewModel Program { get; set; }
        public List<CoursesViewModel> UnmappedCourses { get; set; }
        public Dictionary<int, List<CoursesViewModel>> MappedCoursesByDuration { get; set; }
    }

}