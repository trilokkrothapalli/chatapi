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
            CreateMap<ApplicationUser, UserModel>().ForMember(x => x.DisplayName, map => map.MapFrom(y=>y.FullName))
                                                   .ForMember(x => x.PhotoUrl, map => map.MapFrom(y => y.Avatar))
                                                   .ForMember(x => x.Uid, map => map.MapFrom(y => y.Id.ToString()))
                                                   .ReverseMap();
            CreateMap<IEnumerable<ApplicationUser>, IEnumerable<UserModel>>().ReverseMap();
        }
    }
}
