using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class Routine : BaseEntity
    {
        public int RoutineID { get; set; }
        public int ProgramID { get; set; }
        public int Year { get; set; }
        [MaxLength(7)]
        public string Section { get; set; }
        public int ProgramCourseID { get; set; }
        public int FacultyID { get; set; }
        public string RoomNo { get; set; } = null;
        public string DayOfWeek { get; set; } = null;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public EducationPrograms Program { get; set; } = null;
        public ProgramCourse ProgramCourse { get; set; } = null;
        public Faculty Faculty { get; set; } = null;
    }
}