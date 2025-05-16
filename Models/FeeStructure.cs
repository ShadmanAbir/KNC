using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.Models
{
    public class FeeStructure :BaseEntity
    {
        public int ID { get; set; }
        public int ProgramID { get; set; }
        public string FeeType { get; set; } = null; // Registration, Dorm, etc.
        public decimal Amount { get; set; }
        public string Frequency { get; set; }
    }
}