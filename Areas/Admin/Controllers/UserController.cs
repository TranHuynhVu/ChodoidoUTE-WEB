using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ChodoidoUTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public UserController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("admin/user")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("admin/user/list")]
        public async Task<IActionResult> getList()
        {
            var users = await (from user in _context.Users
                                               join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                               join role in _context.Roles on userRole.RoleId equals role.Id
                                               where role.NormalizedName != "ADMIN"
                                               select new
                                               {
                                                   user.Id,
                                                   user.Email,
                                                   user.ImgUrl,
                                                   user.Name,
                                                   user.UserName,
                                                   user.Status
                                               }).ToListAsync();
            return Json(new
            {
                data = users
            });
        }
        [Route("admin/user/approve/{id}")]
        [HttpPatch]
        public async Task<IActionResult> ApproveUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return Json(new { status = 404, message = "Không tìm thấy người dùng này!" });
            }
            user.Status = 1;
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Json(new { status = 200, message = "Tài khoản đã được duyệt!" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = "Có lỗi xảy ra trong quá trình duyệt tài khoản!", error = ex.Message });
            }

        }
        [Route("admin/user/deny/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DenyUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return Json(new { status = 404, message = "Không tìm thấy người dùng này!" });
            }
            user.Status = -1;
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Json(new { status = 200, message = "Tài khoản đã được khóa!" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = "Có lỗi xảy ra trong quá trình khóa tài khoản!", error = ex.Message });
            }
        }
        [Route("admin/user/detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _context.Users
                        .FirstOrDefaultAsync(p => p.Id == id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Không tồn tại tài khoản này";
                return View("Index");
            }
            ViewBag.User = user;
            return View();
        }
        [Route("admin/user/provide")]
        [HttpPost]
        public async Task<IActionResult> Provide()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "DanhSachSinhVienDangKy.xlsx");

            if (!System.IO.File.Exists(filePath))
            {
                return Json(new { status = 500, message = "Không tồn tại file này!" });
            }

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 6; row <= rowCount; row++) 
                {
                    var studentCode = worksheet.Cells[row, 2].Text; // Mã sinh viên
                    var fullName = worksheet.Cells[row, 3].Text;    // Họ và tên
                    var email = $"{studentCode}@sv.ute.udn.vn";
                    var address = worksheet.Cells[row, 4].Text;
                    var cccd = worksheet.Cells[row, 5].Text;
                    var sdt = worksheet.Cells[row, 6].Text;
                    var dateStr = worksheet.Cells[row, 7].Text;
                    DateTime date = DateTime.ParseExact(dateStr, "dd-MM-yyyy", null);
                    string password = date.ToString("ddMMyyyy");

                    // Kiểm tra tài khoản đã tồn tại
                    var existingUser = await _userManager.FindByEmailAsync(email);
                    if (existingUser == null)
                    {
                        var newUser = new AppUser
                        {
                            UserName = email,
                            Email = email,
                            Name = fullName,
                            Status = 1,
                            ImgUrl = "/img-user.jpg",
                            Address = address,
                            PhoneNumber =sdt,
                            Cccd = cccd,
                        };

                        var result = await _userManager.CreateAsync(newUser,password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(newUser, "User");
                        }
                    }
                }
            }

            return Json(new { status = 200, message = "Tạo tài khoản cho sinh viên thành công!" });
        }
    }
}
