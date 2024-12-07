using ChodoidoUTE.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace ChodoidoUTE.Models
{
    public class ChatsHub : Hub
    {
        // Gửi tin nhắn đến người dùng cụ thể
        public async Task SendToUser(string userId, MessageVM message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; 
          
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
