using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class AssetCategoryRepository : BaseReadOnlyRepository<AssetCategory, Guid>, IAssetCategoryRepository
    {
        public AssetCategoryRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public new string TableName = "AssetCategory";
    }
}
