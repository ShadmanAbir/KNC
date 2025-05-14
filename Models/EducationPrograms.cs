using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class EducationPrograms : BaseEntity
    {
        [Key]
        public int EducationProgramID { get; set; }
        public required string ProgramName { get; set; }
        public required string Description { get; set; }
        public int CourseType { get; set; }
        public required string Duration { get; set; }
    }
}
