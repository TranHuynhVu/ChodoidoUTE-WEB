using Azure.Messaging;
using ChodoidoUTE.Data;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ChodoidoUTE.Models;

namespace ChodoidoUTE.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        public readonly AppDbContext _context;
        public readonly SignInManager<AppUser> _signManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly IChats _chat;
        IHubContext<ChatsHub> _hubContext;
        public ChatsController(IHubContext<ChatsHub> hubContext, IChats chat, AppDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
           _hubContext = hubContext;
            _chat = chat;
        }
        [Route("/chat")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var listChats = await _chat.GetListChatByUserId(user.Id);
            return View(listChats);
        }
        [Route("/chat/product/create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User not authenticated.");
            }
            Product product = await _context.Products
                                       .Include(p => p.User)
                                       .FirstOrDefaultAsync(p => p.Id == id) ;
            
            TinNhan tinnhan = await _context.TinNhans.FirstOrDefaultAsync(t => t.ProductId == id && (t.NguoiGuiId == user.Id || t.NguoiNhanId == user.Id));
            if (tinnhan == null)
            {
                tinnhan = new TinNhan
                {
                    NguoiGuiId = user.Id,
                    NguoiNhanId = product.User.Id,
                    ProductId = product.Id
                };

                await _context.TinNhans.AddAsync(tinnhan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Chats");
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
