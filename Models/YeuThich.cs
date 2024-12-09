using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    [PrimaryKey(nameof(ProductId), nameof(UserId))]
    public class YeuThich
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }

        // Navigation properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }
    }

}
