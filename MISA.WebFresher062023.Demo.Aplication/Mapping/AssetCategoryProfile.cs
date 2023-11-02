using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class AssetCategoryProfile : Profile
    {
        public AssetCategoryProfile() 
        {
            CreateMap<AssetCategory, AssetCategoryDto>();
        }
    }
}
