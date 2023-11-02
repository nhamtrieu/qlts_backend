using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class IncreaseAssetRepository : BaseCrudRepository<IncreaseAsset, Guid>, IIncreaseAssetRepository
    {
        public new string TableName = "IncreaseAsset";
        public IncreaseAssetRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<int> DeleteAssetIncreaseByIncreaseId(Guid increaseId)
        {
            var sql = $"DELETE FROM IncreaseAsset WHERE IncreaseId = @id";
            var param = new DynamicParameters();
            param.Add($"@id", increaseId);

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);

            return result;
        }

        public async Task<List<IncreaseAsset>> GetIncreaseAssetByListIncreaseIds(List<Guid> increaseIds)
        {
            var sql = $"SELECT * FROM IncreaseAsset WHERE IncreaseId IN @increaseIds;";

            var param = new DynamicParameters();
            param.Add("@increaseIds", increaseIds);

            var result = await Uow.Connection.QueryAsync<IncreaseAsset>(sql, param, transaction: Uow.Transaction);

            return result.ToList();
        }

        public async Task<List<AssetIncreaseInfo>> GetListIncreaseDetailByIncreaseId(Guid id)
        {
            var sql = $"SELECT * FROM IncreaseAsset JOIN Asset ON IncreaseAsset.AssetId = Asset.AssetId WHERE IncreaseAsset.IncreaseId = @id";

            var param = new DynamicParameters();
            param.Add("@id", id);

            var result = await Uow.Connection.QueryAsync<AssetIncreaseInfo>(sql, param, transaction: Uow.Transaction);

            return result.ToList();
        }

        public async Task<List<IncreaseAsset>> GetIncreaseAssetByIncreaesId(Guid increaseId)
        {
            var sql = $"SELECT * FROM IncreaseAsset WHERE IncreaseId = @increaseId";
            var param = new DynamicParameters();
            param.Add("@increaseId", increaseId);

            var result = await Uow.Connection.QueryAsync<IncreaseAsset>(sql, param, transaction: Uow.Transaction);

            return result.ToList();
        }

        public async Task<List<IncreaseAsset>> GetIncreaseAssetByIncreaseIdAndAssetIds(Guid inceaseId, List<Guid> assetIds)
        {
            var sql = "SELECT * FROM IncreaseAsset WHERE (IncreaseId = @IncreaseId AND AssetId IN @AssetIds)";

            var param = new DynamicParameters();
            param.Add("@IncreaseId", inceaseId);
            param.Add("@AssetIds", assetIds);

            var result = await Uow.Connection.QueryAsync<IncreaseAsset>(sql, param, transaction: Uow.Transaction);
            return result.ToList();
        }
    }
}
