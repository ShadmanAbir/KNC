using System;

namespace KNC.ViewModels
{
    public class EducationProgramsViewModel : BaseViewModel
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public int DurationInYears { get; set; }
    }
}