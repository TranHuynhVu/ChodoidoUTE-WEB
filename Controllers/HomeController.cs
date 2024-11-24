using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChodoidoUTE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/trang-chu")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/dao-cho")]
        public IActionResult Daocho()
        {
            return View();
        }

        [Route("/detail")]
        public IActionResult Detail()
        {
            return View();
        }
        [Route("/thong-tin-ca-nhan")]
        public IActionResult ThongTinCaNhan()
        {
            return View();
        }
        [Route("/yeu-thich")]
        public IActionResult YeuThich()
        {
            return View();
        }

    }
}
