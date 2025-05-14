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
    }
}
