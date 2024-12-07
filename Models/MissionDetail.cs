using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class MissionDetail
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DateChecked { get; set; }
        public int? MissionId { get; set; }
        public string? UserId { get; set; }

        public virtual Mission Mission { get; set; }
        public virtual AppUser User { get; set; }
    }
}
