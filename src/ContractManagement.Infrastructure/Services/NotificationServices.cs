using ContractManagement.Domain.Entity;
using ContractManagement.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ContractManagement.Infrastructure.Services
{
    public class NotificationServices(IHubContext<NotificationHub> hubContext)
    {
        private readonly IHubContext<NotificationHub> _hubContext = hubContext;

        public async Task SendNotificationAsync(Notification notification)
        {
            await _hubContext.Clients.All.SendAsync("NotificationReceived", notification);
        }
        public async Task SendNoticationUserAsync(Notification notification, string destineUserEmail)
        {
            await _hubContext.Clients.User(destineUserEmail).SendAsync("NotificationReceived", notification);
        }
    }

}
