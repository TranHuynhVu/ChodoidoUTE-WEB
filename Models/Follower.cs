using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    public class Follower
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
