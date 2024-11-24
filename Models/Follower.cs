using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    public class Follower
    {
        [Key]
        public long Id { get; set; }
        public long? IdUser { get; set; }
        public virtual User User { get; set; }
    }
}
