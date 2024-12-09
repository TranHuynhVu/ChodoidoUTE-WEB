using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    public class ProductVideo
    {
        [Key]
        public int Id { get; set; }
        public string? VideoUrl { get; set; }
        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
