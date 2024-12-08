using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Controllers
{
    public class DangTinController : Controller
    {
        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly IProduct _product;
        public DangTinController(IProduct product, AppDbContext context, SignInManager<AppUser> signInManager,
                                UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _product = product;
        }
        [Route("/dang-tin")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/user/product/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var productVM = await _product.GetProductVM(id);
            return View(productVM);
        }

    }
}
