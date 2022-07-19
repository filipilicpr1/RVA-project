using AutoMapper;
using Server.Dto.UserDto;
using Server.Models;

namespace Server.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, DisplayUserDTO>().ReverseMap();
            CreateMap<User, NewUserDTO>().ReverseMap();
            CreateMap<User, AuthDTO>().ReverseMap();
        }
    }
}
