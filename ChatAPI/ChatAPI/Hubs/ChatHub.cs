using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chat.Services.Interfaces;
using Chat.Services.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IApplicationUserService applicationUserService;
        public readonly static List<UserModel> _Connections = new List<UserModel>();
        private readonly static Dictionary<int, string> _ConnectionsMap = new Dictionary<int, string>();
        public ChatHub(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        public Task AddUsers(int userId)
        {
            try
            {
               
                var userViewModel =  applicationUserService.GetUserById(userId).Result;

                if (!_Connections.Any(u => u.Uid == userId.ToString()))
                {
                    _Connections.Add(userViewModel);
                    _ConnectionsMap.Add(userId, Context.ConnectionId);
                    Clients.Caller.SendAsync("getUser", userViewModel);
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            return base.OnConnectedAsync();
        }

        public async Task SendPrivateMessage(int receiverUserId, string message, string dateTime, UserModel sender)
        {
            if (_ConnectionsMap.TryGetValue(receiverUserId, out string connectionId))
            {
                // Who is the sender;

                if (!string.IsNullOrEmpty(message.Trim()))
                {
                    // Build the message
                    var messageViewModel = new MessageModel()
                    {
                        Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty),
                        CreatedAt = dateTime,
                        User = sender

                    };

                    // Send the message
                    await Clients.Client(connectionId).SendAsync("NewMessage", messageViewModel);
                    //await Clients.Caller.SendAsync("NewMessage", messageViewModel);
                }
            }
        }

       

        private int GetId()
        {
            var id = Context.GetHttpContext().Request.Query["userId"];
            return Convert.ToInt32(id);
        }
    }
}
