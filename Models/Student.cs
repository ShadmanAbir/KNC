using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KNC.Models
{
    public class Student : BaseEntity
    {
        [Key]
        public int StudentID { get; set; }
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        // [RegularExpression(@"^(\+8801)[3-9]\d{8}$", ErrorMessage = "Invalid phone number.")]
        public string LinkImage { get; set; }
        public string Phone { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int Program { get; set; }



        [NotMapped]
        public string ProgramName { get; set; }
        [NotMapped]
        public string Name { get; set; }
    }
}