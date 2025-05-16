using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class StudentFee
    {
        public int FeeId { get; set; }
        public int StudentId { get; set; }
        public string FeeType { get; set; } = null; // Registration, Dorm, etc.
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }

        public Student Student { get; set; } = null;
    }
}