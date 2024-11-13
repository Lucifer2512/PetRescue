using Microsoft.AspNetCore.SignalR;

namespace PetRescueFE.SignalRealtime
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
        public async Task SendNotificationToAll(string message)
        {
            // Gửi thông báo cho tất cả người dùng
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
