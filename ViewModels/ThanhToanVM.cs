using ChodoidoUTE.Models;

namespace ChodoidoUTE.ViewModels
{
    public class ThanhToanVM
    {
        public int? Id { get; set; }
        public int? Count { get; set; }
        public double? Price { get; set; }
        public string? Title { get; set; }
        public AppUser? User { get; set; }
        public List<ProductImage>? Images { get; set; }

    }
}
