using System;
using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR.Controllers
{
	public class HubDemo: Hub
	{
        /// 
        /// estos metodos los usa el cliente para enviar mensajes al servidor
        /// 
		public async Task SendMessageToServer(string message)
		{
			Console.WriteLine("Hello I am a web app " + message);
			await Clients.All.SendAsync("ReceiveMessage", "User", message);
        }

        // este sirve para unirce al grupo
        public async Task JoinGroup(string groupName)
        {
            Console.WriteLine("GroupName" + groupName + Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        // sirve para enviar un mensaje al grupo
        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("AdminGroupMessage", groupName, message);
        }
    }
}

