using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Controllers
{
    public class ChatsController : Controller
    {
        [Route("/chat")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
