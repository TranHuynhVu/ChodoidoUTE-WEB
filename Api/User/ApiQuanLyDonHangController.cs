using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Api.User
{
    public class ApiQuanLyDonHangController : Controller
    {
        private readonly AppDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signManager;
        public readonly IThanhToan _thanhToan;
        private readonly IDonHang _donHang;
        public ApiQuanLyDonHangController(IDonHang donHang, IThanhToan thanhToan, AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
            _thanhToan = thanhToan;
            _donHang = donHang;
        }
        [Route("/donhang/huy/{id}")]
        public async Task<IActionResult> Huydon(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var buy = await _context.Buys.FindAsync(id);
            if (buy != null)
            {
                buy.Status = "DA_HUY";
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Hủy đơn hàng thành công" });
            }
            return Json(new { error = true, message = "Hủy đơn hàng thất bại" });
        }
        [Route("/donhang/xacnhan/{id}")]
        public async Task<IActionResult> XacNhan(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var buy = await _context.Buys.FindAsync(id);
            if (buy != null)
            {
                buy.Status = "CAN_THANH_TOAN";
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Xác nhận đơn hàng thành công" });
            }
            return Json(new { error = true, message = "Xác nhận đơn hàng thất bại" });
        }
        [Route("/donhang/xacnhanthanhtoan/{id}")]
        public async Task<IActionResult> XacNhanThanhToan(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var buy = await _context.Buys.FindAsync(id);
            if (buy != null)
            {
                buy.Status = "HOAN_TAT";
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Xác nhận thanh toán đơn hàng thành công" });
            }
            return Json(new { error = true, message = "Xác nhận thanh toán đơn hàng thất bại" });
        }
    }
}
