using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace KNC.Models
{
    public class ProgramCourse :BaseEntity
    {
        public int ProgramCourseID { get; set; }
        public int ProgramID { get; set; }
        public int CourseID { get; set; }
        public int Year { get; set; }

        public EducationPrograms Program { get; set; }
        public Courses Course { get; set; } 

        public ICollection<Routine> Routines { get; set; } = new List<Routine>();
        public ICollection<CourseTeacherAssignment> Assignments { get; set; } = new List<CourseTeacherAssignment>();
        public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    }
}