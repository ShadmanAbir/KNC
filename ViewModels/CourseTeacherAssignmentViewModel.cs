using System;

namespace KNC.ViewModels
{
    public class CourseTeacherAssignmentViewModel : BaseViewModel
    {
        public int AssignmentID { get; set; }
        public int CourseID { get; set; }
        public int FacultyID { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Remarks { get; set; }
    }
}