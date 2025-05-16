using System;

namespace KNC.ViewModels
{
    public class MonthlyFeeViewModel : BaseViewModel
    {
        public int MonthlyFeeID { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime PaidDate { get; set; }
        public string Remarks { get; set; }
    }
}