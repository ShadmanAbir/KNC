using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Faculty : BaseEntity
    {
        [Key]
        public int FacultyID { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public string? Email { get; set; }
        public required string Phone { get; set; }
        public required string PermanentAddress { get; set; }
        public required string CurrentAddress { get; set; }
        public int Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        public required string EducationalQualification { get; set; }
        public int TeachingExperience { get; set; }
        public int ClinicalExperience { get; set; }

        public int LocalPublication { get; set; }
        public int InternationalPublication { get; set; }


    }
}
