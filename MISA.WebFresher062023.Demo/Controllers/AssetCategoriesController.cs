using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetCategoriesControler : BaseReadOnlyController<Guid, AssetCategoryDto>
    {
        private readonly IAssetCategoryService _assetCategoryService;
        public AssetCategoriesControler(IAssetCategoryService readOnlyService) : base(readOnlyService)
        {
            _assetCategoryService = readOnlyService;
        }
    }
}
