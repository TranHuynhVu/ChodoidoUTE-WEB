using ChodoidoUTE.Models;

namespace ChodoidoUTE.ViewModels
{
    public class DanhGiaVM
    {
        public int Id { get; set; } 
        public int? Rate { get; set; }
        public AppUser? User { get; set; }
        public ProductImage? ProductImages { get; set; }
        public Product? Product { get; set; }
    }
}
