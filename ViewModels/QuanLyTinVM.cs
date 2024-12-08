using ChodoidoUTE.Models;

namespace ChodoidoUTE.ViewModels
{
    public class QuanLyTinVM
    {
        public AppUser? User { get; set; }   
        public List<Product>? DA_DUYETS { get; set; }
        public List<Product>? DA_ANS { get; set; }
        public List<Product>? CHO_DUYETS { get; set; }
        public List<Product>? TU_CHOIS {  get; set; }

    }
}
