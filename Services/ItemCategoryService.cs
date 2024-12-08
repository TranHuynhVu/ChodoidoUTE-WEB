using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Services
{
    public class ItemCategoryService : ICategory
    {
        public readonly AppDbContext _context;
        public ItemCategoryService(AppDbContext context) { 
            _context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
