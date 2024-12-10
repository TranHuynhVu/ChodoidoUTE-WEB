using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChodoidoUTE.Controllers
{
    public class PaymentController : Controller
    {
        private IMomoService _momoService;
        private readonly AppDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signManager;
        public readonly IThanhToan _thanhToan;
        public PaymentController(IThanhToan thanhToan, IMomoService momoService, AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
        {
            _momoService = momoService;
            _context = context;
            _userManager = userManager;
            _signManager = signManager;
            _thanhToan = thanhToan;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> CreatePaymentMomo(OrderInfoModel model)
        {
            var response =  await _momoService.CreatePaymentMomo(model);
            return Redirect(response.PayUrl);
        }
        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response =  _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            var requestQuery = HttpContext.Request.Query;
            if (requestQuery["resultCode"] != 0) // giao dịch không thành công
            {
                var newMomoInsert = new OrderInfoModel
                {
                    OrderId = requestQuery["oderId"],
                    FullName = User.FindFirstValue(ClaimTypes.Email),
                    OrderInfor = requestQuery["orderInfor"],
                    Amount = (double)decimal.Parse(requestQuery["amount"]),
                    CreateDay = DateTime.Now
                };
                // Lưu vào csdl
            }
            else
            {
                TempData["error"] = "Đã hủy giao dịch";
                RedirectToAction("ThanhToan", "Home");
            }
            return View(response);
        }
        public async Task<IActionResult> CreatePayCash(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var buy = await _context.Buys.FirstOrDefaultAsync(b => b.Id == id && b.UserId == user.Id);
            if(buy != null)
            {
                buy.Status = "CHO_XAC_NHAN_THANH_TOAN";
                _context.Buys.Update(buy);
                await _context.SaveChangesAsync();
                TempData["success"] = "Chờ xác nhận thanh toán";
                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = "Lỗi thanh toán";

            return RedirectToAction("Index", "Home");
        }
        [Route("/thanh-toan/{id}")]
        public async Task<IActionResult> ThanhToan(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var thanhtoan = await _thanhToan.GetBuyId(id);

            return View(thanhtoan);
        }

    }
}
