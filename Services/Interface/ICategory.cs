using ChodoidoUTE.Models;

namespace ChodoidoUTE.Services.Interface
{
    public interface ICategory
    {
        Task<List<Category>> GetCategories();
    }
}
