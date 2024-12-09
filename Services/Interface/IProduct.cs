using ChodoidoUTE.ViewModels;

namespace ChodoidoUTE.Services.Interface
{
    public interface IProduct
    {
        Task<List<ItemProductVM>> GetProductVMByStatus(string status);
        Task<ItemProductVM> GetProductVM(int id);
        Task<List<ItemProductVM>> GetYeuThichByUserId(string id);
        Task<QuanLyTinVM> GetListTinByUserId(string id);
    }
}
