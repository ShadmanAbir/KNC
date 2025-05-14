using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class PaymentInfo : BaseEntity
    {
        [Key]
        public int PaymentID { get; set; }
        public int StudentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
