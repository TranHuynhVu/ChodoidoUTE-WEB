using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChodoidoUTE.Area.User
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFile _file;
        private readonly IProduct _product;

        public ProductController(IProduct product, AppDbContext context, SignInManager<AppUser> signInManager,
                                UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IFile file)
        {
            _context = context;
            _signManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _file = file;
            _product = product;
        }

        [HttpPost]
        [Route("/user/product/create")]
        public async Task<IActionResult> Create([FromForm] ProductVM productVm)
        {
            if (productVm == null)
            {
                TempData["error"] = "Vui long nhap dau du thong tin";
                return RedirectToAction("Index", "DangTin");
            }

            // Lấy thông tin người dùng hiện tại
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["error"] = "Ban can dang nhap";
                return RedirectToAction("Index", "DangTin");
            }

            // Tạo đối tượng Product
            var product = new Product
            {
                Title = productVm.Title,
                Description = productVm.Description,
                Count = productVm.Count,
                Price = productVm.Price,
                IsNew = productVm.IsNew,
                CategoryId = productVm.CategoryId,
                UserId = user.Id,
                TimePost = DateTime.Now,
                PostProductStatus = "CHO_DUYET"
            };

            // Thêm sản phẩm vào database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Lưu file ảnh
            if (productVm.ImageUpload != null)
            {
                foreach (var image in productVm.ImageUpload)
                {
                    var imagePath = await _file.SaveFile(image, "images");
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var productImage = new ProductImage
                        {
                            ProductId = product.Id,
                            UrlImg = imagePath
                        };
                        _context.ProductImages.Add(productImage);
                    }
                }
            }

            // Lưu file video
            if (productVm.ImageUpload != null)
            {
                foreach (var video in productVm.VideoUpload)
                {
                    var videoPath = await _file.SaveFile(video, "videos"); // Đảm bảo phương thức SaveFile xử lý đúng
                    if (!string.IsNullOrEmpty(videoPath))
                    {
                        var productVideo = new ProductVideo
                        {
                            ProductId = product.Id,
                            VideoUrl = videoPath
                        };
                        _context.ProductsVideo.Add(productVideo);
                    }
                }
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            TempData["success"] = "Dang tin thanh cong";
            return RedirectToAction("Index", "DangTin");
        }

        [HttpPost]
        [Route("/user/product/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductVM model)
        {
            // Retrieve the product from the database
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVideos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Validate inputs
            if (model.ImageUpload == null || model.ImageUpload.Count == 0)
            {
                TempData["error"] = "Sản phẩm cần ít nhất một hình ảnh.";
                return RedirectToAction("Edit", new { id });
            }

            if (model.VideoUpload == null || model.VideoUpload.Count == 0)
            {
                TempData["error"] = "Sản phẩm cần ít nhất một video.";
                return RedirectToAction("Edit", new { id });
            }

            // Update basic product details
            product.Title = model.Title;
            product.Price = model.Price;
            product.Count = model.Count;
            product.Description = model.Description;
            product.IsNew = model.IsNew;
            product.CategoryId = model.CategoryId;

            // Delete old images if new images are uploaded
            if (model.ImageUpload.Any())
            {
                foreach (var oldImage in product.ProductImages.ToList())
                {
                    // Delete the old image file from the server
                    await _file.DeleteFile(oldImage.UrlImg);

                    // Remove old image from database
                    _context.ProductImages.Remove(oldImage);
                }

                // Add new images
                foreach (var image in model.ImageUpload)
                {
                    var imagePath = await _file.SaveFile(image, "images");
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var newImage = new ProductImage
                        {
                            ProductId = product.Id,
                            UrlImg = imagePath
                        };
                        _context.ProductImages.Add(newImage);
                    }
                }
            }

            // Delete old videos if new videos are uploaded
            if (model.VideoUpload.Any())
            {
                foreach (var oldVideo in product.ProductVideos.ToList())
                {
                    // Delete the old video file from the server
                    await _file.DeleteFile(oldVideo.VideoUrl);

                    // Remove old video from database
                    _context.ProductsVideo.Remove(oldVideo);
                }

                // Add new videos
                foreach (var video in model.VideoUpload)
                {
                    var videoPath = await _file.SaveFile(video, "videos");
                    if (!string.IsNullOrEmpty(videoPath))
                    {
                        var newVideo = new ProductVideo
                        {
                            ProductId = product.Id,
                            VideoUrl = videoPath
                        };
                        _context.ProductsVideo.Add(newVideo);
                    }
                }
            }

            // Save changes to the database
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            TempData["success"] = "Cập nhật sản phẩm thành công!";
            return RedirectToAction("Index", "DangTin");
        }


    }
}
