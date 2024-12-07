using ChodoidoUTE.ViewModels;

namespace ChodoidoUTE.Services.Interface
{
    public interface IUser
    {
        Task<List<ItemProductVM>> GetYeuThichByUserId(string id);
        Task<int> GetCountYeuThichByUserId(string id);
    }
}
