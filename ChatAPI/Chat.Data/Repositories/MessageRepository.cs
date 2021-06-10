using Chat.Data.Context;
using Chat.Data.GenericRepository;
using Chat.Data.Models;

namespace Chat.Data.Repositories
{
    public class MessageRepository:ApplicationRepository<Message>
    {
        public MessageRepository(ChatDbContext chatDbContext) :base(chatDbContext)
        {
        }
    }
}
