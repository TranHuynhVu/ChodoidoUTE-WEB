using ChodoidoUTE.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChodoidoUTE.ViewModels
{
    public class TinNhanVM
    {
        public int Id { get; set; }
        public AppUser? NguoiGui { get; set; }
        public AppUser? NguoiNhan { get; set; }
        public Product? Product { get; set; }

        public string? LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public List<ChiTietTinNhan>? chiTietTinNhans { get; set; }
    }
}
