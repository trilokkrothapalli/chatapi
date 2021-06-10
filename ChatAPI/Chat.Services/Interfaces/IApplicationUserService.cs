using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Services.Models;

namespace Chat.Services.Interfaces
{
    public interface IApplicationUserService
    {
        Task<UserModel> GetUserById(int id);

        Task<UserModel> Create(UserModel userModel);

        Task<IEnumerable<UserModel>> GetUsers();
    }
}
