using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class EducationPrograms : BaseEntity
    {
        [Key]
        public int EducationProgramID { get; set; }
        public required string ProgramName { get; set; }
        public required string ShortName { get; set; }
        public required string Duration { get; set; }
    }
}
