using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class SourceRepository : BaseCrudRepository<Source, Guid>, ISourceRepository
    {
        

        public SourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<int> DeleteSourceByIncreaseAssetId(Guid increaseAssetId)
        {
            var sql = $"DELETE FROM Source WHERE IncreaseAssetId = @increaseAssetId";

            var param = new DynamicParameters();
            param.Add("@increaseAssetId", increaseAssetId);

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);

            return result;
        }

        public async Task<int> DeleteSourceByIncreaseAssetIds(List<Guid> assetIncreaseIds)
        {
            var sql = $"DELETE FROM Source WHERE IncreaseAssetId in @increaseAssetIds";

            var param = new DynamicParameters();
            param.Add("@increaseAssetIds", assetIncreaseIds);

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);

            return result;
        }

        public async Task<int> DeleteSourceByIncreaseIdAsync(Guid increaseId)
        {
            var sql = $"DELETE FROM source WHERE IncreaseAssetId IN (SELECT i.IncreaseAssetId FROM increaseasset i WHERE i.IncreaseId = @id);";

            var param = new DynamicParameters();
            param.Add("@id", increaseId);

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);
            return result;
        }

        public async Task<List<Source>> GetSourceByIncreaseIdAsync(Guid increaseId)
        {
            var sql = "SELECT * FROM Source WHERE IncreaseAssetId IN " +
                "(SELECT IncreaseAssetId FROM increaseasset WHERE IncreaseId = @id ORDER BY ModifyDate DESC) LIMIT 1;";
            var param = new DynamicParameters();
            param.Add("@id", increaseId);

            var result = await Uow.Connection.QueryAsync<Source>(sql, param, transaction: Uow.Transaction);

            return result.ToList();    
        }

        public async Task<List<Source>> GetSourcesByIncreaseAssetIdAsync(Guid increaseAssetId)
        {
            var sql = "SELECT * FROM Source WHERE IncreaseAssetId = @increaseAssetId ";
            var param = new DynamicParameters();
            param.Add("@increaseAssetId", increaseAssetId);

            var result = await Uow.Connection.QueryAsync<Source>(sql, param, transaction: Uow.Transaction);

            return result.ToList();
        }

        public async Task<List<Source>> GetSourcetByAssetIdAsync(Guid assetId)
        {
            var sql = "SELECT * FROM Source WHERE IncreaseAssetId IN " +
                 "(SELECT IncreaseAssetId FROM IncreaseAsset WHERE AssetId = @Id) LIMIT 1;";

            var param = new DynamicParameters();
            param.Add("@Id", assetId);

            var result = await Uow.Connection.QueryAsync<Source>(sql, param, transaction: Uow.Transaction);
            return result.ToList();
        }
    }
}
