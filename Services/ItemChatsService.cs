using ChodoidoUTE.Data;
using ChodoidoUTE.Models;
using ChodoidoUTE.Services.Interface;
using ChodoidoUTE.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChodoidoUTE.Services
{
    public class ItemChatsService : IChats
    {
        public readonly AppDbContext _context;
        public ItemChatsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TinNhanVM>> GetListChatByUserId(string userId)
        {
            var tinNhans = await _context.TinNhans
                .Include(t => t.NguoiGui)
                .Include(t => t.NguoiNhan)
                .Include(t => t.Product)
                .ThenInclude(p => p.ProductImages)
                .OrderByDescending(t => t.LastMessageTime)
                .Where(t => t.NguoiGuiId == userId || t.NguoiNhanId == userId)
                .Select(t => new TinNhanVM
                {
                    Id = t.Id,
                    NguoiGui = t.NguoiGui,
                    NguoiNhan = t.NguoiNhan,
                    Product = t.Product,
                  /*  LastMessage = t.chiTietTinNhans
                                  .OrderByDescending(c => c.ThoiGianGui)
                                  .Select(c => c.NoiDung)
                                  .FirstOrDefault(),*/
                    LastMessage = t.LastMessage,                    
                    LastMessageTime = t.LastMessageTime
                })
                .ToListAsync();

            return tinNhans;
        }


        public async Task<TinNhanVM> GetListChiTietTinNhan(int tinNhanId)
        {
            var tinNhan = await _context.TinNhans
                                        .Include(t => t.NguoiGui)
                                        .Include(t => t.NguoiNhan)
                                        .Include(t => t.Product)
                                        .ThenInclude(p => p.ProductImages)
                                        .Where(t => t.Id == tinNhanId)
                                        .Select(t => new TinNhanVM
                                        {
                                            Id = t.Id,
                                            NguoiGui = t.NguoiGui,
                                            NguoiNhan = t.NguoiNhan,
                                            Product = t.Product,
                                            chiTietTinNhans = _context.ChiTietTinNhans
                                                                      .Where(c => c.TinNhanId == t.Id)
                                                                      .OrderBy(c => c.ThoiGianGui)
                                                                      .Select(c => new ChiTietTinNhan
                                                                      {
                                                                          Id = c.Id,
                                                                          TinNhanId = c.TinNhanId,
                                                                          NguoiGuiId = c.NguoiGuiId,
                                                                          NoiDung = c.NoiDung,
                                                                          ThoiGianGui = c.ThoiGianGui,
                                                                          NguoiGui = c.NguoiGui
                                                                      })
                                                                      .ToList()
                                        })
                                        .FirstOrDefaultAsync();

            return tinNhan;
        }

    }
}
