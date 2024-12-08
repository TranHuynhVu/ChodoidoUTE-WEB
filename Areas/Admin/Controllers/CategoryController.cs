using ChodoidoUTE.Areas.Admin.Models;
using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
namespace ChodoidoUTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [Route("/admin/category")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("admin/category/list")]
        public async Task<IActionResult> getList()
        {
            var categories= await _context.Categories.ToListAsync();

            return Json(new
            {
                data = categories
            });
        }

        [Route("admin/category/create")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("admin/category/create")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(String Name, IFormFile Url)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                TempData["ErrorMessage"] = string.Join("; ", errorMessages);
                return View("Create");
            }
            string timestamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Url.FileName);
            string fileExtension = Path.GetExtension(Url.FileName);
            string newFileName = $"{fileNameWithoutExtension}-{timestamp}{fileExtension}";

            // Đường dẫn lưu file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", newFileName);
            // Lưu hình ảnh
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Url.CopyToAsync(stream);
            }

            string imageUrl = "/img/" + newFileName;
            var newCategory = new Category
            {
                Name = Name,
                Url = imageUrl
            };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Danh mục đã được tạo thành công!";
            return RedirectToAction("Index");
        }

        [Route("admin/category/edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            var category= await _context.Categories.FindAsync(id);
            ViewBag.Category = category;
            return View();
        }
        [Route("admin/category/edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditCategory(long id, String Name, IFormFile Url)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy danh mục!";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                TempData["ErrorMessage"] = string.Join("; ", errorMessages);
                ViewBag.Category = category;
                return View("Edit");
            }
            string timestamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Url.FileName);
            string fileExtension = Path.GetExtension(Url.FileName);
            string newFileName = $"{fileNameWithoutExtension}-{timestamp}{fileExtension}";

            // Đường dẫn lưu file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", newFileName);
            // Lưu hình ảnh
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Url.CopyToAsync(stream);
            }
            category.Name = Name;
            category.Url = "/img/" + newFileName;
            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi cập nhật dữ liệu: {ex.Message}";
                ViewBag.Category = category;
                return View("Edit");
            }
        }
        [Route("admin/category/delete/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return Json(new { status = 404, message = "Không tìm thấy danh mục!" });
            }

            if (!string.IsNullOrEmpty(category.Url))
            {
                string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.Url.TrimStart('/'));
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Json(new { status = 200, message = "Xóa danh mục thành công!" });
        }
    }
}
