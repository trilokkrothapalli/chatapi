using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Services.Interfaces;
using Chat.Services.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService userService;

        public UserController(IApplicationUserService applicationUserService) {
            userService = applicationUserService;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create(UserModel user)
        {

            return Ok(await userService.Create(user).ConfigureAwait(false));
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            return Ok(await userService.GetUsers().ConfigureAwait(false));
        }
    }
}
