using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChodoidoUTE.Controllers
{
    public class PaymentController : Controller
    {
        private IMomoService _momoService;
        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
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

    }
}
