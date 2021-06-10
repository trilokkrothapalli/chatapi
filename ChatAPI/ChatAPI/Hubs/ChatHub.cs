using System;
using System.Collections.Generic;
using System.Linq;
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

        public override Task OnConnectedAsync()
        {
            try
            {
                int id = GetId();  
                var userViewModel =  applicationUserService.GetUserById(id).Result;

                if (!_Connections.Any(u => u.Id == id))
                {
                    _Connections.Add(userViewModel);
                    _ConnectionsMap.Add(id, Context.ConnectionId);
                }

                Clients.Caller.SendAsync("getProfileInfo", userViewModel.FullName, userViewModel.Avatar);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = _Connections.Where(u => u.Id == GetId()).First();
                _Connections.Remove(user);

                // Tell other users to remove you from their list
                //Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                // Remove mapping
                _ConnectionsMap.Remove(user.Id);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }

        private int GetId()
        {
            var id = Context.GetHttpContext().Request.Query["userId"];
            return Convert.ToInt32(id);
        }
    }
}
