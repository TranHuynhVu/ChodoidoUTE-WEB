using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class ServicePackage
    {
        [Key]
        public int Id { get; set; }
        public int? CountPost { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public int? Point { get; set; }
        public int? Time { get; set; }

        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
