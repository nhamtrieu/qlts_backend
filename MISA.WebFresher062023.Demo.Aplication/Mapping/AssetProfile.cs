using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<Asset, AssetDto>();

            CreateMap<AssetCreateDto, Asset>();
            CreateMap<AssetUpdateDto, Asset>();
        }
    }
}
