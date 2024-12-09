using ChodoidoUTE.Data;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Services
{
    public class ItemProductService : IProduct
    {
        public readonly AppDbContext _context;
        public ItemProductService(AppDbContext context) {
            _context = context;
        }
        public async Task<List<ItemProductVM>> GetProductVMByStatus(string status)
        {
            var lists = await _context.Products
                .Include(p => p.Category) 
                .Include(p => p.ProductImages) 
                .Include(p => p.ProductVideos) 
                .Include(p => p.User)
                .Where(p => p.PostProductStatus == status)
                .Select(p => new ItemProductVM
                {
                    Id = p.Id,
                    User = p.User,
                    TimePost = p.TimePost,
                    Count = p.Count,
                    Category = p.Category, 
                    Description = p.Description,
                    IsNew = p.IsNew,
                    Price = p.Price,
                    Title = p.Title,
                    Images = p.ProductImages.ToList(), 
                    Videos = p.ProductVideos.ToList()  
                })
                .ToListAsync();

            return lists;
        }

        public async Task<ItemProductVM> GetProductVM(int id)
        {
            var lists = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVideos)
                .Include(p => p.User)
                .Where(p => p.Id == id)
                .Select(p => new ItemProductVM
                {
                    Id = p.Id,
                    User = p.User,
                    TimePost = p.TimePost,
                    Count = p.Count,
                    Category = p.Category,
                    Description = p.Description,
                    IsNew = p.IsNew,
                    Price = p.Price,
                    Title = p.Title,
                    Images = p.ProductImages.ToList(),
                    Videos = p.ProductVideos.ToList()
                })
                .FirstOrDefaultAsync();

            return lists;
        }

        public async Task<List<ItemProductVM>> GetYeuThichByUserId(string id)
        {
            var lists = await _context.YeuThichs
               .Include(y => y.Product)
               .Include(y => y.AppUser)
               .Where(y => y.UserId == id)
               .Select(y => new ItemProductVM
               {
                    Id = y.ProductId,
                    User = y.Product.User,
                    Images = y.Product.ProductImages.ToList(),
                    Title = y.Product.Title,
                    Category = y.Product.Category,
                    Description = y.Product.Description,
                    PostProductStatus = y.Product.PostProductStatus,
                    Count = y.Product.Count,
                    Price = y.Product.Price,
                    IsNew= y.Product.IsNew,
                    TimePost = y.Product.TimePost,
                    Videos = y.Product.ProductVideos.ToList()
               })
               .ToListAsync();

            return lists;
        }

        public async Task<QuanLyTinVM> GetListTinByUserId(string id)
        {
            QuanLyTinVM quanLyTinVM = new QuanLyTinVM();
            quanLyTinVM.User = await _context.Users.FindAsync(id);
            quanLyTinVM.TU_CHOIS = await _context.Products.Include(p => p.ProductImages).OrderByDescending(p => p.TimePost).Where(p => p.UserId == id && p.PostProductStatus == "TU_CHOI").ToListAsync();
            quanLyTinVM.DA_DUYETS = await _context.Products.Include(p => p.ProductImages).OrderByDescending(p => p.TimePost).Where(p => p.UserId == id && p.PostProductStatus == "DA_DUYET").ToListAsync();
            quanLyTinVM.CHO_DUYETS = await _context.Products.Include(p => p.ProductImages).OrderByDescending(p => p.TimePost).Where(p => p.UserId == id && p.PostProductStatus == "CHOI_DUYET").ToListAsync();
            quanLyTinVM.DA_ANS = await _context.Products.Include(p => p.ProductImages).OrderByDescending(p => p.TimePost).Where(p => p.UserId == id && p.PostProductStatus == "DA_AN").ToListAsync();
            return quanLyTinVM;
        }
    }
}
