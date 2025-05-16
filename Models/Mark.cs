using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class Mark : BaseEntity
    {
        public int MarkID { get; set; }
        public int StudentID { get; set; }
        public int ProgramCourseID { get; set; }
        public string ExamName { get; set; } = null; // Midterm, Final, etc.
        public decimal MarksObtained { get; set; }
        public decimal TotalMarks { get; set; }
        public string Grade { get; set; }
        public DateTime ExamDate { get; set; }
        public string Remarks { get; set; }

        public Student Student { get; set; } = null;
        public ProgramCourse ProgramCourse { get; set; } = null;
    }
}