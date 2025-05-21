using AutoMapper;
using KNC.Helper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KNC.ViewModels
{
    public class FeeStructureViewModel : BaseViewModel
    {
        public int ID { get; set; }
        public string FeeType { get; set; }
        public int ProgramID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
         // e.g., Registration, Tuition
        public string Frequency { get; set; } // Use the enum

        public string ProgramName { get; set; }
        [IgnoreMap]
        public IEnumerable<SelectListItem> Programs { get; set; }
    }
}