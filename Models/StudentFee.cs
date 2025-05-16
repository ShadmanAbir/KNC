using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class StudentFee
    {
        [Key]
        public int FeeID { get; set; }
        public int StudentID { get; set; }
        public string FeeType { get; set; } = null; // Registration, Dorm, etc.
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }

        public Student Student { get; set; } = null;
    }
}