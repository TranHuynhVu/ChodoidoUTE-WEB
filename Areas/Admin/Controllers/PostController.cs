using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        public PostController(AppDbContext context)
        {
            _context = context;
        }
        [Route("/admin/post")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("admin/post/list")]
        public async Task<IActionResult> getList()
        {
            var posts = await _context.Products
                 .Select(p => new
                 {
                     p.Id,
                     p.Title,
                     p.PostProductStatus,
                     p.Price,
                     FullName=p.User.Name
                 }).ToListAsync();

            return Json(new
            {
                data = posts
            });
        }

        [Route("admin/post/approve/{id}")]
        [HttpPatch]
        public async Task<IActionResult> ApprovePost(int id)
        {
            var post = await _context.Products.FindAsync(id);
            if (post == null)
            {
                return Json(new { status = 404, message = "Không tìm thấy bài đăng!" });
            }
            post.PostProductStatus = "DA_DUYET";
            try
            {
                _context.Products.Update(post);
                await _context.SaveChangesAsync();
                return Json(new { status = 200, message = "Bài đăng đã được duyệt thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = "Có lỗi xảy ra trong quá trình duyệt bài!", error = ex.Message });
            }

        }
        [Route("admin/post/deny/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DenyPost(int id)
        {
            var post = await _context.Products.FindAsync(id);
            if (post == null)
            {
                return Json(new { status = 404, message = "Không tìm thấy bài đăng!" });
            }
            post.PostProductStatus = "TU_CHOI";
            try
            {
                _context.Products.Update(post);
                await _context.SaveChangesAsync();
                return Json(new { status = 200, message = "Bài đăng đã được từ chối thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = "Có lỗi xảy ra trong quá trình từ chối bài!", error = ex.Message });
            }
        }
        [Route("admin/post/detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var post =  await _context.Products
                        .Include(p => p.User) 
                        .Include(h=> h.Category)
                        .Include(w=> w.ProductImages)
                        .Include(s=>s.ProductVideos)
                        .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                TempData["ErrorMessage"] = "Không tồn tại bài đăng";
                return View("Index");
            }
            ViewBag.Post = post;
            return View();
        }
    }
}
