using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KNC.ViewModels
{
    public class BaseViewModel
    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}