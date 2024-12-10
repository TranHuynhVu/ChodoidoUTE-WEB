using ChodoidoUTE.Models;

namespace ChodoidoUTE.ViewModels
{
    public class ItemProductVM
    {
        public int? Id { get; set; } 
        public int? Count { get; set; }
        public string? Description { get; set; }
        public bool? IsNew { get; set; }
        public double? Price { get; set; }
        public string? Title { get; set; }
        public string? PostProductStatus { get; set; }
        public string? Address { get; set; }

        public DateTime? TimePost { get; set; }
        public AppUser? User { get; set; }
        public  List<ProductImage>? Images {  get; set; }
        public List<ProductVideo>? Videos { get; set; }

        public Category? Category { get; set; }
    }
}
