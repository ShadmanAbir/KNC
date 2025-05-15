using KNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KNC.ViewModel
{
    public class StudentViewModel
    {
        public string Mode { get; set; } // "Create", "Edit", "Details", "Delete"
        public Student Student { get; set; }
        public List<Student> StudentsList { get; set; }
        public SelectList ProgramList { get; set; }
    }
}