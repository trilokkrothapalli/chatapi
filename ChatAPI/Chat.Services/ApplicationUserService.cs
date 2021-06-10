using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chat.Data.GenericRepository.Interface;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using Chat.Services.Models;

namespace Chat.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationRepository<ApplicationUser> userReposiotry;
        private readonly IMapper mapper;
        public ApplicationUserService(IApplicationRepository<ApplicationUser> userReposiotry, IMapper mapper)
        {
            this.userReposiotry = userReposiotry;
            this.mapper = mapper;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return mapper.Map<ApplicationUser,UserModel>(await userReposiotry.GetByIdAsync(id).ConfigureAwait(false));
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserModel>>(await userReposiotry.GetAllAsync().ConfigureAwait(false));
        }

        public async Task<UserModel> Create(UserModel userModel)
        {
            return mapper.Map<ApplicationUser, UserModel>(await userReposiotry.InsertAsync(mapper.Map<UserModel, ApplicationUser>(userModel)).ConfigureAwait(false));
        }
    }
}
