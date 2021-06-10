using Chat.Data.Context;
using Chat.Data.GenericRepository;
using Chat.Data.Models;

namespace Chat.Data.Repositories
{
    public class RoomRepository : ApplicationRepository<Room>
    {
        public RoomRepository(ChatDbContext chatDbContext): base(chatDbContext)
        {
        }
    }
}
