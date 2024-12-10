using ChodoidoUTE.Data;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Services
{
    public class ItemThanhToanService : IThanhToan
    {
        public readonly AppDbContext _context;
        public ItemThanhToanService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ThanhToanVM> GetBuyId(int id)
        {
            var buy = await _context.Buys
               .Include(b => b.Product)
               .ThenInclude(b => b.ProductImages)
               .ThenInclude(b => b.Product.User)
               .Where(b => b.Id == id)
               .Select(b => new ThanhToanVM
               {
                   Id = b.Id,
                   Count = b.Count, 
                   User = b.User,
                   Price = b.Product.Price,
                   Title = b.Product.Title,
                   Images = b.Product.ProductImages.ToList(),
               })
               .FirstOrDefaultAsync();
            return buy;
        }
    }
}
