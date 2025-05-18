using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KNC.ViewModels
{
    public class CoursesViewModel 
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; } 
        public string CourseName { get; set; } 
        public string Description { get; set; }
    }
}
