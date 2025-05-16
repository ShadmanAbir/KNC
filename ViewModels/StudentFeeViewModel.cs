using System;

namespace KNC.ViewModels
{
    public class StudentFeeViewModel : BaseViewModel
    {
        public int StudentFeeID { get; set; }
        public int StudentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public string Remarks { get; set; }
    }
}