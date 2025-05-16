using System;
using System.Collections.Generic;
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
        public string LinkImage { get; set; }
        public string Phone { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int ProgramID { get; set; }

        public EducationPrograms Program { get; set; } = null;
        public ICollection<StudentEnrollment> Enrollments { get; set; } = new List<StudentEnrollment>();
        public ICollection<MonthlyFee> MonthlyFees { get; set; } = new List<MonthlyFee>();
        public ICollection<StudentFee> StudentFees { get; set; } = new List<StudentFee>();
        public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    }
}