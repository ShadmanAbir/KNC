using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KNC.ViewModels
{
    public class FacultyViewModel : BaseViewModel
    {
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        public string Description { get; set; }

        public int DesignationID { get; set; } // Selected designation
        public IEnumerable<SelectListItem> Designations { get; set; } // For dropdown
    }
}