using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;

namespace KNC.Models
{
    public class Faculty : BaseEntity
    {
        [Key]
        public int FacultyID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public int DesignationID { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EducationalQualification { get; set; }

        public Courses Course { get; set; }
        public ICollection<Routine> Routines { get; set; } = new List<Routine>();
        public ICollection<CourseTeacherAssignment> Assignments { get; set; } = new List<CourseTeacherAssignment>();

    }
}
