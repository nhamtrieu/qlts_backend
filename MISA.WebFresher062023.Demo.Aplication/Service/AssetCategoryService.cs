using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class AssetCategoryService : BaseReadOnlyService<AssetCategory, Guid, AssetCategoryDto>, IAssetCategoryService
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IMapper _mapper;
        public AssetCategoryService(IAssetCategoryRepository repository, IMapper mapper) : base(repository)
        {
            _assetCategoryRepository = repository;
            _mapper = mapper;
        }

        public override string IncrementNumberString(string numberString)
        {
            throw new NotImplementedException();
        }

        public override AssetCategoryDto MapEntityToEntityDto(AssetCategory entity)
        {
            var assetCategoryDto = _mapper.Map<AssetCategoryDto>(entity);
            return assetCategoryDto;
        }
    }
}
