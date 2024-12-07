using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int? Count { get; set; }
        public string? Description { get; set; }
        public bool? IsNew { get; set; }
        public string? PostProductStatus { get; set; } // CHO_DUYET, DA_AN, DA_BAN, DA_DUYET, DA_TUCHOI
        public double? Price { get; set; }
        public DateTime? TimePost { get; set; }
        public string? Title { get; set; }
        public int? CategoryId { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductVideo> ProductVideos { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
    }
}
