using ChodoidoUTE.Areas.Admin.Models;
using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(AppDbContext context, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _signInManager= signInManager;
        }
        [Route("admin/profile")]
        public IActionResult Index()
        {
            return View("Profile");
        }
        [Route("admin/profile/update")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserAdminVM user)
        {
            if (!ModelState.IsValid)
            {
                var firstErrorMessage = ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .FirstOrDefault();

                TempData["ErrorMessage"] = firstErrorMessage;
                return View("Profile");
            }
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = "Người dùng không tồn tại!";
                return View("Profile");
            }
            currentUser.Name = user.Name;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Gender = user.Gender ?? currentUser.Gender; // Giữ giá trị cũ nếu null.
            currentUser.Local = user.Local;
            if (user.ImgUrl != null) {
                string timestamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(user.ImgUrl.FileName);
                string fileExtension = Path.GetExtension(user.ImgUrl.FileName);
                string newFileName = $"{fileNameWithoutExtension}-{timestamp}{fileExtension}";

                // Đường dẫn lưu file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", newFileName);
                // Lưu hình ảnh
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await user.ImgUrl.CopyToAsync(stream);
                }
                string imageUrl = "/img/" + newFileName;
                currentUser.ImgUrl= imageUrl;
            }
            if (!string.IsNullOrEmpty(user.Password) && user.Password == user.ConfirmPassword)
            {
                var passwordHasher = new PasswordHasher<AppUser>();
                currentUser.PasswordHash = passwordHasher.HashPassword(currentUser, user.Password);
            }
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            return View("Profile");
        }
        [Route("admin/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
    
}
