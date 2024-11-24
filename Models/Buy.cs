using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class Buy
    {
        [Key]
        public long Id { get; set; }
        public long? Count { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; } // CHO_XAC_NHAN, DANG_GIAO_DICH, THANH_CONG, THAT_BAI
        public DateTime? TimeBuy { get; set; }
        public long? UserId { get; set; }
        public long? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
