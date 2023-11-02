using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;
using MISA.WebFresher062023.Demo.Domain.Resource;
using static Dapper.SqlMapper;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class AssetRepository : BaseCrudRepository<Asset, Guid>, IAssetRepository
    {
        public new string TableName = "Asset";
        public AssetRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<Asset> FindAssetAsync(Guid assetCode)
        {
            var sql = $"SELECT * FROM {TableName} WHERE AssetCode = @AssetCode;";
            var param = new DynamicParameters();
            param.Add("AssetId", assetCode);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<Asset>(sql, param, transaction: Uow.Transaction);
            return result;
        }

        public async Task<Asset?> FindByCodeAsync(string code)
        {
            var param = new DynamicParameters();
            param.Add("code", code);
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Code = @code;";
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<Asset>(sql, param, transaction: Uow.Transaction);
            if(result == null)
            {
                return result;
            } else
            {
                throw new ConflictException(Resource.ConflictAssetCode);
            }
        }

        public async Task<Asset?> GetLastRecord()
        {
            var sql = $"SELECT * FROM {TableName} WHERE AssetCode LIKE 'ts%' AND AssetCode REGEXP '^ts[0-9]+$' ORDER BY AssetCode DESC LIMIT 1;";
            var result = await Uow.Connection.QueryFirstAsync<Asset>(sql, transaction: Uow.Transaction);
            return result;
        }

        public async Task<FilterAsset> FilterAssetAsync(FilterAsset filter, List<Guid> ids)
        {
            var sql = "SELECT * FROM asset WHERE " +
              "(AssetName LIKE @SearchString OR AssetCode LIKE @SearchString) AND DepartmentName LIKE @DepartmentName AND AssetCategoryName LIKE @AssetCategoryName AND AssetId NOT IN @ids " +
              "ORDER BY AssetCode DESC " +
              "LIMIT @Limit, @Offset;" +
              "SELECT COUNT(*) FROM asset WHERE (AssetName LIKE @SearchString OR AssetCode LIKE @SearchString) AND DepartmentName LIKE @DepartmentName AND AssetCategoryName LIKE @AssetCategoryName AND AssetId NOT IN @ids;";
            var param = new DynamicParameters();
            param.Add("@SearchString", $"%{filter.SearchString}%");
            if (ids == null) ids = new List<Guid>();
            param.Add("@ids", ids);
            if(filter.DepartmentName != null)
            {
                param.Add("@DepartmentName", $"{filter.DepartmentName}%");
            } else
            {
                param.Add("@DepartmentName", $"%%");
            }

            if(filter.AssetCategoryName != null)
            {
                param.Add("@AssetCategoryName", $"{filter.AssetCategoryName}%");
            } else
            {
                param.Add("@AssetCategoryName", $"%%");
            }

            param.Add("@Limit", (filter.PageNumber - 1) * filter.PageSize);
            param.Add("@Offset", filter.PageSize);

            var result = await Uow.Connection.QueryMultipleAsync(sql, param, transaction: Uow.Transaction);

            filter.Data = result.Read<Asset>().ToList();
            filter.TotalRecord = result.ReadFirst<int>();

            return filter;
        }

        public async Task<List<Asset>> FilterBaseAsync(FilterAsset baseFilter)
        {
            var sql = "SELECT * FROM asset WHERE " +
              "(AssetName LIKE @SearchString OR AssetCode LIKE @SearchString) AND DepartmentName LIKE @DepartmentName AND AssetCategoryName LIKE @AssetCategoryName " +
              "ORDER BY AssetCode DESC;";
            var param = new DynamicParameters();
            param.Add("@SearchString", $"{baseFilter.SearchString}%");
            if (baseFilter.DepartmentName != null)
            {
                param.Add("@DepartmentName", $"{baseFilter.DepartmentName}%");
            }
            else
            {
                param.Add("@DepartmentName", $"%%");
            }

            if (baseFilter.AssetCategoryName != null)
            {
                param.Add("@AssetCategoryName", $"{baseFilter.AssetCategoryName}%");
            }
            else
            {
                param.Add("@AssetCategoryName", $"%%");
            }

            var result = await Uow.Connection.QueryAsync<Asset>(sql, param, transaction: Uow.Transaction);
            return result.ToList();
        }

        public async Task<List<Increase>> GetIncreaseByAssetIdAsync(Guid assetId)
        {
            var sql = "SELECT Increase.* FROM Increase " +
                "INNER JOIN IncreaseAsset ON Increase.IncreaseId = IncreaseAsset.IncreaseId " +
                "WHERE IncreaseAsset.AssetId = @assetId";

            var param = new DynamicParameters();
            param.Add("@assetId", assetId);

            var result = await Uow.Connection.QueryAsync<Increase>(sql, param, transaction: Uow.Transaction);

            return result.ToList();
        }

        public async Task<List<Guid>> GetIncreaseByListAssetIdsAsync(List<Guid> assetIds)
        {
            var sql = "SELECT DISTINCT IncreaseAsset.AssetId FROM IncreaseAsset " +
                "WHERE IncreaseAsset.AssetId IN @assetIds";

            var param = new DynamicParameters();
            param.Add("@assetIds", assetIds);

            var result = await Uow.Connection.QueryAsync<Guid>(sql, param, transaction: Uow.Transaction);

            return result.ToList();
        }
    }
}
