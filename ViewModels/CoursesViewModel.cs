using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KNC.ViewModels
{
    public class CoursesViewModel : BaseViewModel
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; } 
        public string CourseName { get; set; } 
        public string Description { get; set; }


        //for mapping with program
        public int Year { get; set; }
    }
}
