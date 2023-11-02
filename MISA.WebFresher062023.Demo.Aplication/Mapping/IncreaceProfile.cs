using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaceProfile : Profile
    {
        public IncreaceProfile()
        {
            CreateMap<IncreaseCreateDto, Increase>();
            CreateMap<IncreasePutDto, Increase>();
            CreateMap<Increase, IncreaseDto>();
            CreateMap<IncreaseJoin, IncreaseDto>();
            CreateMap<IncreaseUpdateDto, Increase>();
        }
    }
}
