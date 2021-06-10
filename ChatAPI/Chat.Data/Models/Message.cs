using System;
using Chat.Data.Models.Interface;

namespace Chat.Data.Models
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public ApplicationUser FromUser { get; set; }
        public int ToRoomId { get; set; }
        public Room ToRoom { get; set; }

        public string GetEntityName()
        {
            return "Messages";
        }
    }
}
