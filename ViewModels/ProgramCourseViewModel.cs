using System;

namespace KNC.ViewModels
{
    public class ProgramCourseViewModel : BaseViewModel
    {
        public int ProgramCourseID { get; set; }
        public int ProgramID { get; set; }
        public int CourseID { get; set; }
        public int Semester { get; set; }
        public string Remarks { get; set; }
    }
}