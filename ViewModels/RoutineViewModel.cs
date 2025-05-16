using System;

namespace KNC.ViewModels
{
    public class RoutineViewModel : BaseViewModel
    {
        public int RoutineID { get; set; }
        public int ProgramID { get; set; }
        public int Year { get; set; }
        public string Section { get; set; }
        public int ProgramCourseID { get; set; }
        public int FacultyID { get; set; }
        public string RoomNo { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}