using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class ProductImage
    {
        [Key]
        public long Id { get; set; }
        public string UrlImg { get; set; }
        public long? IdProduct { get; set; }

        public virtual Product Product { get; set; }
    }
}
