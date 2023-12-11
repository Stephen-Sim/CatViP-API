using CatViP_API.Models;
using Microsoft.AspNetCore.SignalR;

namespace CatViP_API.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {
            
        }

        public async Task SendPrivateMessage(string sendUser, string receiveUser, string message)
        {
            await Clients.User(receiveUser).SendAsync($"ReceiveMessageFrom{sendUser}", message);
        }

        public async Task OnConnect(string username)
        {
            await Clients.User(username).SendAsync("OnConnect");
        }

        public async Task OnDisconnect(string username)
        {
            await Clients.User(username).SendAsync("OnDisconnect");
        }
    }
}
