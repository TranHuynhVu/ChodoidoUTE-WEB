using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class MissionDetail
    {
        [Key]
        public long Id { get; set; }
        public DateTime? DateChecked { get; set; }
        public long? MissionId { get; set; }
        public long? UserId { get; set; }

        public virtual Mission Mission { get; set; }
        public virtual User User { get; set; }
    }
}
