using ChodoidoUTE.Areas.Admin.Models;
using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChodoidoUTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("admin/login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/login")]
        public async Task<IActionResult> Login(LoginVM loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    var passwordHasher = new PasswordHasher<User>();
                    var hashedPassword = passwordHasher.HashPassword(user, loginModel.Password);

                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin"))
                        {
                            TempData["SuccessMessage"] = "Đăng nhập thành công!";
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Bạn không có quyền truy cập!";
                            return RedirectToAction("Index", "Login");
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Mật khẩu không đúng.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Tài khoản không tồn tại.";
                }
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
