using ChodoidoUTE.ViewModels;

namespace ChodoidoUTE.Services.Interface
{
    public interface IDonHang
    {
        Task<DonMuaVM> GetDonMuas(string user);
        Task<DonBanVM> GetDonBans(string user);
    }
}
