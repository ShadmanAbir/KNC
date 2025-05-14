using System;
using System.ComponentModel.DataAnnotations;

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
        public int Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EducationalQualification { get; set; }
        public int TeachingExperience { get; set; }
        public int ClinicalExperience { get; set; }

        public int LocalPublication { get; set; }
        public int InternationalPublication { get; set; }

    }
}
