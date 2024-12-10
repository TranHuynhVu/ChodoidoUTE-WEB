using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Api.User
{
    public class ApiThongTinCaNhanController : Controller
    {
        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public ApiThongTinCaNhanController(AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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
    }
}
