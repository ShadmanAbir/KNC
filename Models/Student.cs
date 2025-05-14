using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Student : BaseEntity
    {
        [Key]
        public int StudentID { get; set; }
        public string StudentCode { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public string? Email { get; set; }
        [RegularExpression(@"^(\+8801)[3-9]\d{8}$", ErrorMessage = "Invalid phone number.")]
        public required string Phone { get; set; }
        public required string PermanentAddress { get; set; }
        public required string CurrentAddress { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int Program { get; set; }

    }
}