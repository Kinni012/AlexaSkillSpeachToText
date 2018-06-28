using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients?.All?.SendAsync("broadcastMessage", name, message);
        }

        public async Task SendAsync(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }


    }
}