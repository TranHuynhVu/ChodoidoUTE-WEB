using Azure.Core;
using ChodoidoUTE.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private AppDbContext _context;
        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        [Route("/admin/dashboard")]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            ViewBag.productCount = products.Count();
            ViewBag.userCount = await _context.Users.CountAsync();
            ViewBag.donHang = await _context.Buys.CountAsync();
            ViewBag.ChartProduct = new Dictionary<string, int>
                    {
                        { "Chờ duyệt", products.Count(r => r.PostProductStatus == "CHO_DUYET") },
                        { "Đã ẩn", products.Count(r => r.PostProductStatus == "DA_AN") },
                        { "Đã duyệt", products.Count(r => r.PostProductStatus == "DA_DUYET") },
                        { "Từ chố", products.Count(r => r.PostProductStatus == "TU_CHOI") },
                        { "Đã xóa", products.Count(r => r.PostProductStatus == "DA_XOA") }
                    };

            return View();
        }
    }
}
