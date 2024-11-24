using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class ServiceDetail
    {
        [Key]
        public long Id { get; set; }
        public DateTime? Expiration { get; set; }
        public long? ServicePackageId { get; set; }
        public long? UserId { get; set; }

        public virtual ServicePackage ServicePackage { get; set; }
        public virtual User User { get; set; }
    }
}
