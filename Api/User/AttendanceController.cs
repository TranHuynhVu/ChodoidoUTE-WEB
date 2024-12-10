using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Api.User
{
    [Route("attendance")]
    public class AttendanceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AttendanceController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Lấy trạng thái điểm danh
        [HttpGet("status")]
        public async Task<IActionResult> GetAttendanceStatus()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var today = DateTime.Today;

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.DateChecked.Date == today);

            return Json(new
            {
                hasChecked = attendance != null,
                points = 100, // Điểm mặc định
                dayOfWeek = today.DayOfWeek
            });
        }

        // Điểm danh
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var today = DateTime.Today;

            // Kiểm tra đã điểm danh chưa
            var existingAttendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.UserId == user.Id && a.DateChecked.Date == today);

            if (existingAttendance != null)
            {
                return Json(new { success = false, message = "Bạn đã điểm danh hôm nay." });
            }

            // Thêm bản ghi điểm danh mới
            var attendance = new Attendance
            {
                UserId = user.Id,
                DateChecked = today
            };
            user.Point += 100;

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Điểm danh thành công!", pointsEarned = 100 });
        }
        [HttpPost("convert")]
        public async Task<IActionResult> ConvertPoints()
        {
            // Lấy thông tin user hiện tại (giả sử bạn đã sử dụng Identity)
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Người dùng không xác định." });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);

            if (user == null)
            {
                return Json(new { success = false, message = "Không tìm thấy người dùng." });
            }

            const int PointsPerCredit = 300; // Điểm cần để đổi 1 lượt đăng tin

            if (user.Point < PointsPerCredit)
            {
                return Json(new { success = false, message = "Không đủ điểm để đổi lượt đăng tin." });
            }

            // Thực hiện trừ điểm và tăng lượt đăng tin
            user.Point -= PointsPerCredit;
            user.LuotDang = (user.LuotDang ?? 0) + 1;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đổi điểm thành công!", points = user.Point, luotDang = user.LuotDang });
        }
    }
}
