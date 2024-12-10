using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _product;
        private readonly AppDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signManager;
        public readonly IThanhToan _thanhToan;
        public HomeController(IThanhToan thanhtoan, SignInManager<AppUser> signManager, UserManager<AppUser> userManager, AppDbContext context, ILogger<HomeController> logger, IProduct product)
        {
            _logger = logger;
            _product = product;
            _context = context;
            _signManager = signManager;
            _userManager = userManager;
            _thanhToan = thanhtoan;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/dao-cho")]
        public IActionResult Daocho()
        {
            return View();
        }

        [Route("/product/detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        { 
            ItemProductVM itemProductVM = await _product.GetProductVM(id);
            return PartialView("~/Views/Home/Detail.cshtml", itemProductVM);
        }

        [Route("/diem-danh")]
       public IActionResult DiemDanh()
       {
            return View();
       }

    }
}
