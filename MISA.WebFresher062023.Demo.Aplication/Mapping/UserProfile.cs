using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>();
        }
    }
}
