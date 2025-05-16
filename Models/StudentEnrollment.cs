using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class StudentEnrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int ProgramCourseID { get; set; }
        public string AcademicYear { get; set; } = null;
        public int Year { get; set; }
        public string EnrollmentStatus { get; set; } = "Enrolled";
        public DateTime EnrollmentDate { get; set; }
        public DateTime? DropDate { get; set; }
        public string Remarks { get; set; }

        public Student Student { get; set; } = null;

        public ICollection<ProgramCourse> ProgramCourses { get; set; } = new List<ProgramCourse>();
    }
}