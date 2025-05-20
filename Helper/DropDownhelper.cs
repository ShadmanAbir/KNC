using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace KNC.Helper
{
    public static class DropDownHelper
    {
        public static List<SelectListItem> GetFrequencyTypes(string selectedValue = null)
        {
            return Enum.GetValues(typeof(FrequencyType))
                .Cast<FrequencyType>()
                .Select(f => new SelectListItem
                {
                    Text = f.ToString(),
                    Value = f.ToString(),
                    Selected = f.ToString() == selectedValue
                })
                .ToList();
        }
    }
}