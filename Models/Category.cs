using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
