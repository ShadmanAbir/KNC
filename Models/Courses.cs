using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Courses : BaseEntity
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseCode { get; set; } 
        public string CourseName { get; set; } 
        public string Description { get; set; }
/*        public int ProgramID { get; set; }

        public EducationPrograms Programs { get; set; }*/
        public ICollection<ProgramCourse> ProgramCourses { get; set; } = new List<ProgramCourse>();
    }
}
