using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Designation : BaseEntity
    {
        [Key]
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public string Description { get; set; }
    }
}
