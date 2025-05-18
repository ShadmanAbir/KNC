using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KNC.ViewModels
{
    public class FacultyViewModel : BaseViewModel
    {
        public int FacultyID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Current Address")]
        public string CurrentAddress { get; set; }

        [Display(Name = "Designation")]
        public int DesignationID { get; set; }

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [Display(Name = "Educational Qualification")]
        public string EducationalQualification { get; set; }

        // Navigation Properties for dropdowns and related data
        public IEnumerable<SelectListItem> Designations { get; set; }

        // Additional properties for display
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        // Optional: Include these if you need to show related data
        public IEnumerable<CourseTeacherAssignmentViewModel> Assignments { get; set; }
        public IEnumerable<RoutineViewModel> Routines { get; set; }
    }

}