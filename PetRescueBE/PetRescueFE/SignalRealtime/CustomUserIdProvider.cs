using Microsoft.AspNetCore.SignalR;

namespace PetRescueFE.SignalRealtime
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Lấy userID từ thông tin kết nối
            var userId = connection.GetHttpContext().Request.Query["userId"].ToString();
            return userId;
        }
    }
}
