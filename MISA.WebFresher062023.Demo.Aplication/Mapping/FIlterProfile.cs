using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class FIlterProfile : Profile
    {
        public FIlterProfile() 
        {
            CreateMap<FilterAsset, FilterAssetDto>();
            CreateMap<FilterAssetCreateDto, FilterAsset>();
            CreateMap<FilterIncrease, IncreaseFilterDto>();
        }
    }
}
