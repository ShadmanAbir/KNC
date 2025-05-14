using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Sessions
    {
        [Key]
        public int SessionID { get; set; }
        public string SessionCode { get; set; }
        public int Program { get; set; }
        public int MyProperty { get; set; }
    }
}
