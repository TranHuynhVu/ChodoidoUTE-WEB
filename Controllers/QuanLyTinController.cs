using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Controllers
{
    [Authorize]
    public class QuanLyTinController : Controller
    {
        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly IProduct _product;
        public QuanLyTinController(IProduct product,AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _product = product;
        }
        [Route("/quan-ly-tin")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var quanlytin = await _product.GetListTinByUserId(user.Id);
            return View(quanlytin);
        }

        [Route("/yeu-thich")]
        public async Task<IActionResult> YeuThich()
        {
            var user = await _userManager.GetUserAsync(User);
            var YeuThichs = await _product.GetYeuThichByUserId(user.Id);
            if (YeuThichs == null)
            {
                return View();
            }
            return View(YeuThichs);
        }
    
    }
}
