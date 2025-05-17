using System;

namespace KNC.ViewModels
{
    public class EducationProgramsViewModel : BaseViewModel
    {
        public int EducationProgramID { get; set; }
        public string ProgramName { get; set; }
        public string ShortName { get; set; }
        public int Duration { get; set; }
    }
}