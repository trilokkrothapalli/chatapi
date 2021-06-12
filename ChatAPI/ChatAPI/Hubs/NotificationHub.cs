using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly static Dictionary<int, string> _ConnectionsMap = new Dictionary<int, string>();

        public async Task Notify(string message)
        {
            await Clients.All.SendAsync(message);
        }
    }
}
