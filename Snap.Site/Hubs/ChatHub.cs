using Microsoft.AspNetCore.SignalR;

namespace Snap.Site.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string userName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }
    }
}
