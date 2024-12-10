using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        private readonly AppDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signManager;
        public readonly IThanhToan _thanhToan;
        private readonly IDonHang _donHang;
        public QuanLyDonHangController(IDonHang donHang, IThanhToan thanhToan, AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
        {
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
            _thanhToan = thanhToan;
            _donHang = donHang;
        }
        [Route("/don-mua")]
        public async Task<IActionResult> DonMua()
        {
            var user = await _userManager.GetUserAsync(User);
            var donmua = await _donHang.GetDonMuas(user.Id);
            return View(donmua);
        }
        [Route("/donhang/mua/{id}/{soluong}")]
        public async Task<IActionResult> MuaHang(int id, int soluong = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (soluong <= 0)
            {
                soluong = 1;
            }
            var buy = new Buy
            {
                ProductId = id,
                Count = soluong,
                Status = "CHO_XAC_NHAN",
                TimeBuy = DateTime.Now,
                Type = "DON_MUA",
                UserId = user.Id,
                Price = product.Price * soluong
            };

            _context.Buys.Add(buy);
            await _context.SaveChangesAsync();

            TempData["success"] = "Chờ xác nhận";

            return RedirectToAction("Index", "Home");

        }
        
        [HttpPost("/donhang/danhgia")]
        public async Task<IActionResult> DanhGia([FromBody] RateStar request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (request == null || request.Rating < 1 || request.Rating > 5)
            {
                return BadRequest(new { error = true, message = "Thông tin đánh giá không hợp lệ" });
            }


            var product = _context.Products.FirstOrDefault(b => b.Id == request.ProductId);
            if (product == null)
            {
                return NotFound(new { error = true, message = "Không tìm thấy sản phẩm" });
            }

            var rate = new RateStar
            {
                ProductId = request.ProductId,
                Rating = request.Rating,
                UserId = user.Id
            };
            _context.RateStars.Add(rate);
            await _context.SaveChangesAsync();

            return Ok(new { error = false, message = "Đánh giá thành công!" });
        }

        [Route("/don-ban")]
        public async Task<IActionResult> DonBan()
        {
            var user = await _userManager.GetUserAsync(User);
            var donban = await _donHang.GetDonBans(user.Id);
            return View(donban);
        }
       
    }
}
