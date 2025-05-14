using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Courses : BaseEntity
    {
        [Key]
        public int CourseID { get; set; }
        public required string CourseName { get; set; }
        public required string Description { get; set; }
        public required decimal Fee { get; set; }
        public required string Duration { get; set; }
    }
}
