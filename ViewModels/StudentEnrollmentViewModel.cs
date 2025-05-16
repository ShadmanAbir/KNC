using System;

namespace KNC.ViewModels
{
    public class StudentEnrollmentViewModel : BaseViewModel
    {
        public int StudentID { get; set; }
        public int ProgramCourseID { get; set; }
        public int AcademicYear { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Remarks { get; set; }
    }
}