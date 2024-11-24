using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class ProductVideo
    {
        [Key]
        public long Id { get; set; }
        public string VideoUrl { get; set; }
        public long? IdProduct { get; set; }

        public virtual Product Product { get; set; }
    }
}
