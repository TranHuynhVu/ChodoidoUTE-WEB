using ChodoidoUTE.Data;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Services
{
    public class ItemUserService : IUser
    {
        public readonly AppDbContext _context;
        public ItemUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCountYeuThichByUserId(string id)
        {
            return await _context.YeuThichs.Where(y => y.UserId == id).CountAsync();
        }

        public async Task<List<DanhGiaVM>> GetDanhGiaById(string id)
        {
            var list = await _context.RateStars
                .Include(r => r.Product)                  
                    .ThenInclude(p => p.User)             
                .Include(r => r.Product.ProductImages)  
                .Include(r => r.User)
                .Where(r => r.Product.UserId == id)       
                .Select(r => new DanhGiaVM
                {
                    Id = r.Id,
                    Product = r.Product,                         
                    ProductImages = r.Product.ProductImages.FirstOrDefault(),
                    Rate = r.Rating,
                    User = r.User
                })
                .ToListAsync(); 

            return list;
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
                   IsNew = y.Product.IsNew,
                   TimePost = y.Product.TimePost,
                   Videos = y.Product.ProductVideos.ToList()
               })
               .ToListAsync();

            return lists;
        }
    }
}
