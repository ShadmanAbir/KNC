using System.ComponentModel.DataAnnotations;

namespace KNC.Models
{
    public class Certificates :BaseEntity
    {
        [Key]
        public int CertificateID { get; set; }
        public int HolderID { get; set; }
        public string CertificateName { get; set; }
        public string HolderCode { get; set; }
        public string Location { get; set; }
    }
}
