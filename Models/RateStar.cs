using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    public class RateStar
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? UserId { get; set; }
        public int? Rating { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
