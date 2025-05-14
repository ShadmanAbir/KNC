using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Certificates :BaseEntity
    {
        [Key]
        public int CertificateID { get; set; }
        public int HolderID { get; set; }
        public required string CertificateName { get; set; }
        public required string HolderCode { get; set; }
        public required string Location { get; set; }
    }
}
