using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChodoidoUTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        [Route("/admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
