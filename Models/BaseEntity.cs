using System;

namespace KNC.Models
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
