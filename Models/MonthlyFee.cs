using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class MonthlyFee : BaseEntity
    {
        public int MonthlyFeeID { get; set; }
        public int StudentID { get; set; }
        public string MonthYear { get; set; } = null; //2025-05
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public string PaymentStatus { get; set; } = "Unpaid"; // Paid, Partial, etc.
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }

        public Student Student { get; set; } = null;
    }
}