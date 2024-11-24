using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Controllers
{
    public class DangTinController : Controller
    {
        [Route("/dang-tin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
