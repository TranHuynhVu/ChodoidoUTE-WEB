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
      
        [Route("/password/edit")]
        public ActionResult EditPassWord()
        {
            return View();
        }      
        [Route("/danh-gia")]
        public IActionResult DanhGia()
        {
            return View();
        }
    }
}
