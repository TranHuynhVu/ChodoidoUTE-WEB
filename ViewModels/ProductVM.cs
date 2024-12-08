using ChodoidoUTE.Models;

namespace ChodoidoUTE.ViewModels
{
    public class ProductVM
    {
        public int? Count { get; set; }
        public string? Description { get; set; }
        public bool? IsNew { get; set; }
        public double? Price { get; set; }
        public string? Title { get; set; }
        public int? CategoryId { get; set; }

        public List<IFormFile>? ImageUpload { get; set; }
        public List<IFormFile>? VideoUpload { get; set; }
    }

}
