using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool? IsMissionDefault { get; set; }
        public string? MissionType { get; set; } // NAM, NGAY, THANG, TUYCHON
        public string? Name { get; set; }
        public int? Point { get; set; }

        public virtual ICollection<MissionDetail> MissionDetails { get; set; }
    }
}
