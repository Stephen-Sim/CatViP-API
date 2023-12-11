using CatViP_API.Models;
using CatViP_API.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CatViP_API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendPrivateMessage(string sendUser, string receiveUser, string message)
        {
            await Clients.User(receiveUser).SendAsync($"ReceiveMessageFrom{sendUser}", message);

            await _chatService.StoreChat(sendUser, receiveUser, message);
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
