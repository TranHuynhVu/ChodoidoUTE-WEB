using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChodoidoUTE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _product;
        public HomeController(ILogger<HomeController> logger, IProduct product)
        {
            _logger = logger;
            _product = product;
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
        [Route("/thong-tin-ca-nhan")]
        public IActionResult ThongTinCaNhan()
        {
            return View();
        }

    }
}
