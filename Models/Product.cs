using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public long? Count { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public string PostProductStatus { get; set; } // CHO_DUYET, DA_AN, DA_BAN, DA_DUYET, DA_TUCHOI
        public double? Price { get; set; }
        public DateTime? TimePost { get; set; }
        public string Title { get; set; }
        public long? IdCategory { get; set; }
        public long? IdUser { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductVideo> ProductVideos { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
    }
}
