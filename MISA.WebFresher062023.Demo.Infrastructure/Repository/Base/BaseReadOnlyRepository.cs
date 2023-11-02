using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public abstract class BaseReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        protected readonly IUnitOfWork Uow;
        protected virtual string TableName { get; set; } = typeof(TEntity).Name;

        public BaseReadOnlyRepository(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName} ORDER BY ModifyDate DESC;";
            var result = (await Uow.Connection.QueryAsync<TEntity>(sql, transaction: Uow.Transaction)).ToList();
            return result;
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            var entity = await FindAsync(id);
            if(entity is null)
            {
                throw new NotFoundException("Không tìm thấy dữ liệu!");
            }
            return entity;
        }
        public async Task<TEntity> FindAsync(TKey id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id;";
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<TEntity>(sql, param, transaction: Uow.Transaction);
            return result;
        }

        public async Task<List<TEntity>> GetByListIdAsync(List<TKey> ids)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id IN @ids";

            var param = new DynamicParameters();
            param.Add("@ids", ids);

            var result = await Uow.Connection.QueryAsync<TEntity>(sql, param, transaction: Uow.Transaction);

            return result.ToList();
        }

        public async Task<(List<TEntity>, List<TKey>)> GetManyAsync(List<TKey> ids)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id in @Ids";
           
            var param = new DynamicParameters();
            param.Add("@Ids", ids);

            var entities = (await Uow.Connection.QueryAsync<TEntity>(sql, param, transaction: Uow.Transaction)).ToList();
            var retrievedIds = entities.Select(entity => entity.GetId()).ToList();
            var remainingIds = ids.Except(retrievedIds).ToList();
            return (entities, remainingIds);
        }

        public async Task<TEntity> GetLastEntityAsync()
        {
            var sql = $"SELECT * FROM {TableName} ORDER BY ModifyDate DESC LIMIT 1;";
            var result = await Uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, transaction: Uow.Transaction);
            return result;
        }
    }
}
