using ChodoidoUTE.ViewModels;

namespace ChodoidoUTE.Services.Interface
{
    public interface IThanhToan
    {
        Task<ThanhToanVM> GetBuyId(int id);
    }
}
