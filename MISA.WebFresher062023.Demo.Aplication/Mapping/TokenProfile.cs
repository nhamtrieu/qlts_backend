using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class TokenProfile : Profile
    {
        public TokenProfile() 
        {
            CreateMap<TokenDto, TokenReturnDto>();
            CreateMap<TokenReturnDto, TokenDto>();
        }
    }
}
