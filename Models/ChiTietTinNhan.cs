using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.Models
{
    public class ChiTietTinNhan
    {
        public int Id { get; set; }
        public int TinNhanId { get; set; }
        [ForeignKey("TinNhanId")]
        public TinNhan TinNhan { get; set; }
        public string NguoiGuiId { get; set; }
        [ForeignKey("NguoiGuiId")]
        public virtual AppUser NguoiGui {  set; get; }
        public string NoiDung {  get; set; }
        public DateTime ThoiGianGui {  get; set; }

    }
}
