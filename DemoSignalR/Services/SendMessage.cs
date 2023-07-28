using System;
using DemoSignalR.Controllers;
using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR.Services
{
    public interface ISendMessage
    {
        Task ReceiveMessage();
        Task SendMessageToGroup(string groupName, string messag);
    }

    public class SendMessage : ISendMessage
    {

        private readonly IHubContext<HubDemo> _hubContext;

        public SendMessage(IHubContext<HubDemo> hubContext)
        {
            this._hubContext = hubContext;
        }

        public async Task ReceiveMessage()
        {
            await this._hubContext.Clients.All.SendAsync("ReceiveMessage", "User", "Hello, clients, I am back!");
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {

            await this._hubContext.Clients.Group(groupName).SendAsync("AdminGroupMessage", groupName, message);
        }
    }
}

