using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;
using System.Collections.Generic;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class IncreaseRepository : BaseCrudRepository<Increase, Guid>, IIncreaseRepository
    {
        public new string TableName = "increase";
        private readonly IAssetRepository _assetRepository;
        private readonly IIncreaseAssetRepository _increaseDetailRepository;
        private readonly ISourceRepository _sourceRepository;
        public IncreaseRepository(IUnitOfWork uow, IAssetRepository assetRepository, IIncreaseAssetRepository increaseDetailRepository, ISourceRepository sourceRepository) : base(uow)
        {
            _assetRepository = assetRepository;
            _increaseDetailRepository = increaseDetailRepository;
            _sourceRepository = sourceRepository;
        }

        public Task<List<Increase>> GetIncreasesByIncreaseAssetIdAsync(Guid increasesId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetMaxIncreaseCodeAsync()
        {
            var sql = "SELECT IncreaseCode FROM Increase ORDER BY ModifiedDate DESC LIMIT 1";

            var result = await Uow.Connection.QueryFirstOrDefaultAsync<string>(sql, transaction: Uow.Transaction);

            return result;
        }

        public async Task<Tuple<List<IncreaseJoin>, int>> GetPaginIncreaseAsync(int limit, int offset)
        {
           
            var sql = "SELECT i.*, SUM(id.IncreaseCost) AS TotalCost FROM Increase AS i " +
                "INNER JOIN IncreaseAsset AS id on i.IncreaseId = id.IncreaseId " +
                "INNER JOIN Asset AS a ON id.AssetId = a.AssetId " +
                "GROUP BY i.IncreaseId ORDER BY i.ModifyDate DESC LIMIT @limit OFFSET @offset;" +
                "SELECT COUNT(*) FROM Increase";    

            var param = new DynamicParameters();
            param.Add("@limit", limit);
            param.Add("@offset", offset);

            var multi = await Uow.Connection.QueryMultipleAsync(sql, param, transaction: Uow.Transaction);
            var entities = await multi.ReadAsync<IncreaseJoin>();
            var countentity = await multi.ReadSingleOrDefaultAsync<int>();

            return (entities.ToList(),  countentity).ToTuple();
        }

        public async Task<Tuple<List<IncreaseJoin>, int>> SearchIncreaseAsync(string key, int limit, int offset)
        {
            var param = new DynamicParameters();
            param.Add("@Key", $"%{key}%");

            var limitSql = String.Empty;

            if(limit > 0)
            {
                param.Add("@offset", offset);
                param.Add("@limit", limit);
                limitSql = "LIMIT @limit OFFSET @offset";
            }

            var sql = "SELECT i.*, SUM(a.Cost) AS TotalCost FROM Increase AS i " +
                "INNER JOIN IncreaseAsset AS id on i.IncreaseId = id.IncreaseId " +
                "INNER JOIN Asset AS a ON id.AssetId = a.AssetId WHERE (i.IncreaseCode LIKE @Key OR i.Description LIKE @Key)" +
                $"GROUP BY i.IncreaseId ORDER BY i.ModifyDate DESC {limitSql};" +
                "SELECT COUNT(*) FROM Increase WHERE (IncreaseCode LIKE @Key OR Description LIKE @Key)";

            var multi = await Uow.Connection.QueryMultipleAsync(sql, param, transaction: Uow.Transaction);
            var assets = await multi.ReadAsync<IncreaseJoin>();
            var countEntity = await multi.ReadSingleOrDefaultAsync<int>();
            return (assets.ToList(), countEntity).ToTuple();
        }

        public async Task<FilterIncrease> FilterIncreaseAysnc(FilterIncrease filterIncrease)
        {
            var sql = "SELECT i.* FROM Increase AS i " +
                "WHERE (Content LIKE @SearchString OR IncreaseCode LIKE @SearchString) " +
                "ORDER BY i.IncreaseCode DESC " +
                "LIMIT @Limit, @Offset; " +
                "SELECT COUNT(*) FROM Increase WHERE (Content LIKE @SearchString OR IncreaseCode LIKE @SearchString);";
            var param = new DynamicParameters();

            var searchString = filterIncrease.SearchString == "%%" ? "%%" : $"%{filterIncrease.SearchString}%";

            param.Add("@SearchString", searchString);
            param.Add("@limit", (filterIncrease.PageNumber - 1) * filterIncrease.PageSize);
            param.Add("@offset", filterIncrease.PageSize);

            var result = await Uow.Connection.QueryMultipleAsync(sql, param, transaction: Uow.Transaction);
            filterIncrease.Data = result.Read<Increase>().ToList();
            filterIncrease.TotalRecord = result.ReadFirst<int>();

            return filterIncrease;
        }

        public async Task<Increase?> FindByCodeAsync(string code)
        {
            var sql = $"SELECT * FROM Increase WHERE IncreaseCode = @code";

            var param = new DynamicParameters();
            param.Add("@code", code);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<Increase>(sql, param, transaction: Uow.Transaction);

            return result;
        }

    }
}
