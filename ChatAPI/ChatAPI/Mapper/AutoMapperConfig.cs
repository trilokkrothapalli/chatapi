using AutoMapper;
using Chat.Services.MappingProfile;

namespace ChatAPI.Mapper
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Config()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile<UserProfile>();
            }
            );
        }

        public static IMapper Create()
        {
            return Config().CreateMapper();
        }
    }

    
}
