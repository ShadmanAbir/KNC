using System;

namespace KNC.ViewModels
{
    public class MarkViewModel : BaseViewModel
    {
        public int MarkID { get; set; }
        public int StudentID { get; set; }
        public int ProgramCourseID { get; set; }
        public string ExamName { get; set; }
        public decimal MarksObtained { get; set; }
        public decimal TotalMarks { get; set; }
        public string Grade { get; set; }
        public DateTime ExamDate { get; set; }
        public string Remarks { get; set; }
    }
}