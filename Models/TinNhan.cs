using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChodoidoUTE.Models
{
    public class TinNhan
    {
        [Key]
        public int Id { get; set; } 
        public string NguoiGuiId { get; set; } 
        [ForeignKey("NguoiGuiId")]
        public virtual AppUser NguoiGui { get; set; }
        public string NguoiNhanId { get; set; } 
        [ForeignKey("NguoiNhanId")]
        public virtual AppUser NguoiNhan { get; set; }
        public int ProductId { get; set; } 
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        // Thông tin về tin nhắn cuối cùng
        public string? LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public ICollection<ChiTietTinNhan> chiTietTinNhans {  get; set; } 
       
    }
}
