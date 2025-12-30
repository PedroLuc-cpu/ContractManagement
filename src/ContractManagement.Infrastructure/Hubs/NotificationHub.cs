using ContractManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace ContractManagement.Infrastructure.Hubs
{
    [SignalRHub]
    public class NotificationHub(UserManager<ApplicationUser> userManager) : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendNotification(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", title, message);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Nova conexão: {Context.ConnectionId}");

            var user = await _userManager.FindByEmailAsync("pedrolucas@bemasoft.com.br");
            if (user is not null)
            {
                user.StatusUser = StatusUserEnum.Online;
                user.Entrada = DateTime.UtcNow;

                await _userManager.UpdateAsync(user);
                
            }
            
            await Clients.Caller.SendAsync("ReceiveMessage", "Sistema", "Bem-vindo ao chat!" + user.Email);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _userManager.FindByEmailAsync("pedrolucas@bemasoft.com.br");
            if (user is not null)
            {
                user.StatusUser = StatusUserEnum.Offline;
                user.Entrada = DateTime.UtcNow;

                await _userManager.UpdateAsync(user);

            }
            Console.WriteLine($"Conexão encerrada: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
