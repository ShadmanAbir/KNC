using KNC.Helper;
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

        public string FeeType { get; set; } // e.g., Registration, Tuition
        public string Frequency { get; set; } // Use the enum
    }
}