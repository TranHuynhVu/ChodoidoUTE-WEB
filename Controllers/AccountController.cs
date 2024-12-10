using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.View_Models;
using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Controllers
{
    public class AccountController : Controller
    {
        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/dang-nhap")]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var role = await _userManager.GetRolesAsync(user);
                if (role.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (role.Contains("User"))
                {
                    return RedirectToAction("trang-chu");
                }
                else
                {
                    return RedirectToAction("trang-chu");
                }
            }
            return View();
        }
        [Route("/account/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM userVM)
        {
            var userEmail = userVM.Email.Trim().ToUpper();
            var user = await _userManager.FindByNameAsync(userVM.Email);
            if (user != null)
            {
                if (user.Status == 1)
                {
                    //login
                    var result = await _signManager.PasswordSignInAsync(userEmail!, userVM.Password!, false, false);
                    if (result.Succeeded)
                    {
                        var role = await _userManager.GetRolesAsync(user);
                        if (role.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        }
                        else if (role.Contains("User"))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng nhập thất bại!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản chưa được kích hoạt");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại");
            }
            return View(userVM);
        }
        [Route("/dang-ky")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("/account/register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(registerVM.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email đã được đăng ký.");
                    return View(registerVM);
                }

                var newUser = new AppUser
                {
                    UserName = registerVM.Email,
                    Email = registerVM.Email,
                    Name = registerVM.Name,
                    Point = 0,
                    LuotDang = 3,
                    Status = 0,
                    ImgUrl = "/img-user.jpg"
                };

                var result = await _userManager.CreateAsync(newUser, registerVM.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
                    await _signManager.SignInAsync(newUser, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(registerVM);
        }
        [Route("/dang-xuat")]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
