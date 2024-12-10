using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Controllers
{
    public class ThongTinCaNhanController : Controller
    {
        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public ThongTinCaNhanController(AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Route("/thong-tin-ca-nhan")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/chinh-sua")]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var profileVM = new ProfileVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    TaxNumber = user.TaxNumber
                };
                return View(profileVM);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("/profile/edit")]
        public async Task<IActionResult> Edit(ProfileVM profileVM)
        {
            var user = await _userManager.FindByIdAsync(profileVM.Id);
            if (user != null)
            {
                user.Email = profileVM.Email;
                user.UserName = profileVM.Email;
                user.Name = profileVM.Name;
                user.Address = profileVM.Address;
                user.PhoneNumber = profileVM.PhoneNumber;
                user.Cccd = profileVM.Cccd;
                user.TaxNumber = profileVM.TaxNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["success"] = "Cập nhật thông tin thành công";
                    return View(profileVM);
                }
                TempData["error"] = "Cập nhật thông tin thất bại";
            }
            return View(profileVM);
        }
        [Route("/mang-xa-hoi")]
        public ActionResult Mxh()
        {
            return View();
        }
        [Route("/mang-xa-hoi/edit")]
        [HttpPost]
        public async Task<IActionResult> Mxh(string SocialMedia, string SocialMediaUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Kiểm tra xem mạng xã hội đã có hay chưa
                if (SocialMedia == "Facebook" && !string.IsNullOrEmpty(SocialMediaUrl))
                {
                    user.Facebook = SocialMediaUrl;
                    await _userManager.UpdateAsync(user);

                    // Trả về kết quả thành công dưới dạng JSON
                    return Json(new { success = true, message = "Thêm Facebook thành công!" });
                }
                else if (SocialMedia == "Instagram" && !string.IsNullOrEmpty(SocialMediaUrl))
                {
                   /* user.Instagram = SocialMediaUrl;*/
                    await _userManager.UpdateAsync(user);

                    return Json(new { success = true, message = "Thêm Instagram thành công!" });
                }
                else
                {
                    return Json(new { success = false, message = "Chức năng đang cập nhật hoặc dữ liệu không hợp lệ!" });
                }
            }
            return Json(new { success = false, message = "Người dùng không tồn tại!" });
        }
        [Route("/mang-xa-hoi/delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteSocialMedia(string SocialMedia)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (SocialMedia == "Facebook")
                {
                    user.Facebook = null;
                }
                else if (SocialMedia == "Instagram")
                {
                   // user.Instagram = null;
                }
                else if (SocialMedia == "Twitter")
                {
                    //user.Twitter = null;
                }
                else if (SocialMedia == "Linkedin")
                {
                   // user.Linkedin = null;
                }
                else
                {
                    return Json(new { success = false, message = "Mạng xã hội không hợp lệ!" });
                }

                await _userManager.UpdateAsync(user);

                return Json(new { success = true, message = $"{SocialMedia} đã được xóa thành công!" });
            }
            return Json(new { success = false, message = "Người dùng không tồn tại!" });
        }

        [Route("/password/edit")]
        public ActionResult EditPassWord()
        {
            return View();
        }
        [Route("/password/edit")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "Người dùng không tồn tại." });
            }

            // Thay đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Mật khẩu đã được thay đổi thành công!" });
            }

            // Xử lý lỗi
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Json(new { success = false, errors });
        }

        [Route("/danh-gia")]
        public IActionResult DanhGia()
        {
            return View();
        }
    }
}
