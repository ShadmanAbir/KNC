using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.ViewModels
{
    public class StudentsViewModel : BaseViewModel
    {
        public int StudentID { get; set; }
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string LinkImage { get; set; }
        public string Phone { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int ProgramID { get; set; }
        
    }
}