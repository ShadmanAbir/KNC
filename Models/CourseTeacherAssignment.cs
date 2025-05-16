using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class CourseTeacherAssignment : BaseEntity
    {
        public int AssignmentID { get; set; }
        public int ProgramCourseID { get; set; }
        public int FacultyID { get; set; }
        public string PreferredDays { get; set; }
        public string PreferredTimeSlot { get; set; }

        public ProgramCourse ProgramCourse { get; set; } = null;
        public Faculty Faculty { get; set; } = null;
    }
}