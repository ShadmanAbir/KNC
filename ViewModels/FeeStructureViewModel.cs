using System;

namespace KNC.ViewModels
{
    public class FeeStructureViewModel : BaseViewModel
    {
        public int FeeStructureID { get; set; }
        public int ProgramID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
    }
}