using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    public class ServiceDetail
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Expiration { get; set; }
        public int? ServicePackageId { get; set; }
        public string? UserId { get; set; }

        public virtual ServicePackage ServicePackage { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
