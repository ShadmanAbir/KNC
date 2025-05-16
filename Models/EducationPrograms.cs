using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class EducationPrograms : BaseEntity
    {
        [Key]
        public int EducationProgramID { get; set; }
        public string ProgramName { get; set; }
        public string ShortName { get; set; }
        public string Duration { get; set; }

        public ICollection<ProgramCourse> ProgramCourses { get; set; } = new List<ProgramCourse>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Routine> Routines { get; set; } = new List<Routine>();
    }
}
