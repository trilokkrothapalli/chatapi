using System;
using System.Collections.Generic;
using AutoMapper;
using Chat.Data.Models;
using Chat.Services.Models;

namespace Chat.Services.MappingProfile
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserModel>().ReverseMap();
            CreateMap<IEnumerable<ApplicationUser>, IEnumerable<UserModel>>().ReverseMap();
        }
    }
}
