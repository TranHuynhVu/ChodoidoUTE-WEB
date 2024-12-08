using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Api.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanlytinController : Controller
    {

        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly IChats _chat;
        public QuanlytinController(IChats chat, AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _chat = chat;
        }
        [Route("/quanlytin/yeuthich/{id}")]
        public async Task<IActionResult> SetYeuThich(int id)
        {
            var AppUser = await _userManager.GetUserAsync(User);
            var result = await _context.YeuThichs.AddAsync(new YeuThich { ProductId = id, UserId = AppUser.Id });
            if (result != null)
            {
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Thêm vào yêu thích thành công" });
            }
            else
            {
                return Json(new { error = true, message = "Thêm vào yêu thích thất bại" });
            }
        }
        [Route("/quanlytin/product/antin/{id}")]
        public async Task<IActionResult> SetTinDangAn(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.UserId == user.Id && p.Id == id);
            if (product != null)
            {
                product.PostProductStatus = "DA_AN";
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Ẩn tin thành công" });
            }
            return Json(new { error = true, message = "Ẩn tin thất bại" });
        }
        [Route("/quanlytin/product/xoatin/{id}")]
        public async Task<IActionResult> SetTinDangXoa(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.UserId == user.Id && p.Id == id);
            if (product != null)
            {
                product.PostProductStatus = "DA_XOA";
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Xóa tin thành công" });
            }
            return Json(new { error = true, message = "Xóa tin thất bại" });
        }

        [Route("/quanlytin/product/boyeuthich/{id}")]
        public async Task<IActionResult> BoYeuThich(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var yeuthich = await _context.YeuThichs.FirstOrDefaultAsync(y => y.UserId == user.Id && y.ProductId == id);
            if (yeuthich != null)
            {
                _context.Remove(yeuthich);
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Bỏ yêu thích thành công" });
            }
            return Json(new { error = true, message = "Bỉ yêu thích thất bại" });
        }
        [Route("/quanlytin/product/suatin/{id}")]
        public async Task<IActionResult> SuaTin(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var yeuthich = await _context.YeuThichs.FirstOrDefaultAsync(y => y.UserId == user.Id && y.ProductId == id);
            if (yeuthich != null)
            {
                _context.Remove(yeuthich);
                await _context.SaveChangesAsync();
                return Json(new { error = false, message = "Bỏ yêu thích thành công" });
            }
            return Json(new { error = true, message = "Bỉ yêu thích thất bại" });
        }
    }
}


