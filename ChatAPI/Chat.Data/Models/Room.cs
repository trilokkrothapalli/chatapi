using System.Collections.Generic;
using Chat.Data.Models.Interface;

namespace Chat.Data.Models
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser Admin { get; set; }
        public ICollection<Message> Messages { get; set; }

        public string GetEntityName()
        {
            return "Rooms";
        }
    }
}
