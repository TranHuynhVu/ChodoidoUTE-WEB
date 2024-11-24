using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Controllers
{
    public class QuanLyTinController : Controller
    {
        [Route("/quan-ly-tin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
