using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class Buy
    {
        [Key]
        public long Id { get; set; }
        public int? Count { get; set; }
        public double? Price { get; set; }
        public string? Status { get; set; } // CHO_XAC_NHAN, DANG_GIAO_DICH, THANH_CONG, THAT_BAI
        public DateTime? TimeBuy { get; set; }
        public string? UserId { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual AppUser User { get; set; }
    }
}
