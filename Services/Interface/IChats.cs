using ChodoidoUTE.Models;
using ChodoidoUTE.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ChodoidoUTE.Services.Interface
{
    public interface IChats
    {
        Task<List<TinNhanVM>> GetListChatByUserId(string id);
        Task<TinNhanVM> GetListChiTietTinNhan(int id);
    }
}
