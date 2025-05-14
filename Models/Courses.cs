using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Courses : BaseEntity
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public string Duration { get; set; }
    }
}
