using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseDetailProfile : Profile
    {
        public IncreaseDetailProfile() 
        {
            CreateMap<IncreaseAssetCreateDto, IncreaseAsset>();
            CreateMap<IncreaseAssetUpdateDto, IncreaseAsset>();
            CreateMap<IncreaseAsset, IncreaseAssetDto>();
            CreateMap<AssetIncreaseInfo, IncreaseAssetDto>();
        }
    }
}
