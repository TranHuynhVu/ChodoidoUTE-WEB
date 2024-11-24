using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class ServicePackage
    {
        [Key]
        public long Id { get; set; }
        public long? CountPost { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public long? Point { get; set; }
        public long? Time { get; set; }

        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
