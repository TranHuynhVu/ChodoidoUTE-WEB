using ChodoidoUTE.Data;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Services
{
    public class ItemDonHangService : IDonHang
    {
        private readonly AppDbContext _context;
        public ItemDonHangService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DonBanVM> GetDonBans(string user)
        {
            var list = await _context.Buys.Include(b => b.User)
                                   .Include(b => b.Product)
                                   .ThenInclude(p => p.ProductImages)
                                   .Where(b => b.Product.UserId == user && b.Type == "DON_MUA" && b.ProductId == b.Product.Id)
                                   .ToListAsync();
            var Donban = new DonBanVM
            {
                CHO_XAC_NHAN_THANH_TOANS = list.Where(l => l.Status == "CHO_XAC_NHAN_THANH_TOAN").ToList(),
                CAN_THANH_TOANS = list.Where(l => l.Status == "CAN_THANH_TOAN").ToList(),
                CHO_XAC_NHANS = list.Where(l => l.Status == "CHO_XAC_NHAN").ToList(),
                HOAN_TATS = list.Where(l => l.Status == "HOAN_TAT").ToList()
            };
            return Donban;
        }

        public async Task<DonMuaVM> GetDonMuas(string user)
        {
            var list = await _context.Buys.Include(b => b.User)
                                    .Include(b => b.Product)
                                    .ThenInclude(p => p.ProductImages)
                                    .Where(b => b.UserId == user && b.Type == "DON_MUA")
                                    .ToListAsync();
            var Donmua = new DonMuaVM
            {
                CAN_THANH_TOANS = list.Where(l => l.Status == "CAN_THANH_TOAN").ToList(),
                CHO_XAC_NHANS = list.Where(l => l.Status == "CHO_XAC_NHAN").ToList(),
                DA_HUYS = list.Where(l => l.Status == "DA_HUY").ToList(),
                HOAN_TATS = list.Where(l => l.Status == "HOAN_TAT").ToList()
            };
            return Donmua;
        }
    }
}
