using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Api.User
{
    public class ApiChatsController : Controller
    {
        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly IChats _chat;
        IHubContext<ChatsHub> _hubContext;
        public ApiChatsController(IHubContext<ChatsHub> hubContext, IChats chat, AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _hubContext = hubContext;
            _chat = chat;
        }
        [Route("/chat/product/{id}")]
        public async Task<IActionResult> GetListChiTietTinNhan(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            TinNhan tinnhan = await _context.TinNhans.FirstOrDefaultAsync(t => t.Id == id);
            if (tinnhan == null)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                tinnhan = new TinNhan
                {
                    LastMessage = "",
                    LastMessageTime = DateTime.Now,
                    NguoiGuiId = user.Id,
                    NguoiNhanId = product.User.Id
                };
                await _context.TinNhans.AddAsync(tinnhan);
                _context.SaveChanges();
            }

            var listChitiet = await _chat.GetListChiTietTinNhan(tinnhan.Id);
            return PartialView("~/Views/Chats/ChiTietTinNhan.cshtml", listChitiet);
        }
        [Route("/chat/product/sent")]
        public async Task<IActionResult> SentMessage([FromBody] MessageVM message)
        {
            var user = await _userManager.GetUserAsync(User);
            if (message == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Lỗi gửi tin nhắn"
                });
            }
            var tinnhan = await _context.TinNhans.FindAsync(message.Id);
            if (tinnhan == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Không tìm thấy đoạn chat"
                });
            }
            var chitiettinnhan = new Models.ChiTietTinNhan
            {
                TinNhanId = tinnhan.Id,
                NguoiGuiId = user.Id,
                NoiDung = message.Message,
                ThoiGianGui = DateTime.Now
            };

            _context.ChiTietTinNhans.Add(chitiettinnhan);
            tinnhan.LastMessage = chitiettinnhan.NoiDung;
            tinnhan.LastMessageTime = chitiettinnhan.ThoiGianGui;
            await _context.SaveChangesAsync();

            await _hubContext.Clients.User(message.NguoiNhanId).SendAsync("ReceiveMessage", message);
            return Json(new
            {
                error = false,
                message = ""
            });
        }
    }
}
