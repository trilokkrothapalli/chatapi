using System;
using System.Collections.Generic;
using Chat.Data.Models.Interface;


namespace Chat.Data.Models
{
    public class ApplicationUser : IEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Message> Messages { get; set; }

        public string GetEntityName()
        {
            return "ApplicationUser";
        }
    }
}
