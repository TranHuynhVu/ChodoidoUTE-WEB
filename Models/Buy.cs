using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class Buy
    {
        [Key]
        public int Id { get; set; }
        public int? Count { get; set; }
        public double? Price { get; set; }
        public string? Status { get; set; } // CHO_XAC_NHAN, CHUA_THANH_TOAN, CHO_XAC_NHAN_THANH_TOAN, HOAN_TAT, DA_HUY
        public DateTime? TimeBuy { get; set; }
        public string? UserId { get; set; }
        public int? ProductId { get; set; }
        public string? Type {  get; set; } // DON_MUA, DON_BAN
        public virtual Product Product { get; set; }
        public virtual AppUser User { get; set; }
    }
}
