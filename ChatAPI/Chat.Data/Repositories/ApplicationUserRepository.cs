using Chat.Data.Context;
using Chat.Data.GenericRepository;
using Chat.Data.Models;

namespace Chat.Data.Repositories
{
    public class ApplicationUserRepository: ApplicationRepository<ApplicationUser>
    {
        public ApplicationUserRepository(ChatDbContext chatDbContext):base(chatDbContext)
        {
        }
    }
}
