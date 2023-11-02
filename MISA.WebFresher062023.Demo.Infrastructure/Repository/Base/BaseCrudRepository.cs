using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;
using System.Data;
using System.Data.Common;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public abstract class BaseCrudRepository<TEntity, TKey> : BaseReadOnlyRepository<TEntity, TKey>, ICrudRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        protected BaseCrudRepository(IUnitOfWork uow) : base(uow)
        {

        }
        public async Task<int> InsertAsync(TEntity entity)
        {
            var props = entity.GetType().GetProperties();
            var param = new DynamicParameters();

            var columns = new List<string>();
            var paramsName = new List<string>();

            foreach ( var prop in props)
            {
                var propsName = prop.Name;
                param.Add($"{propsName}", prop.GetValue(entity));
                columns.Add(propsName);
                paramsName.Add($"@{propsName}");
            }

            var columnString = string.Join(", ", columns);
            var paramsNameString = string.Join (", ", paramsName);

            var sql = $"INSERT INTO {TableName} ({columnString}) VALUES ({paramsNameString});";

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);
            return result;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            // Lấy danh sách property của entity
            var props = entity.GetType().GetProperties();
            var param = new DynamicParameters();

            var paramKeyValue = new List<string>();
            foreach (var prop in props)
            {
                var propsName = prop.Name;
                var value = prop.GetValue(entity);
                param.Add($"{propsName}", value);
                if (value == null) continue;
                if(propsName != "AssetId") paramKeyValue.Add($"{propsName} = @{propsName}");
            }
            
            var paramKeyValueString = string.Join(", ", paramKeyValue);

            var sql = $"UPDATE {TableName} SET {paramKeyValueString} WHERE {TableName}Id = @{TableName}Id;";

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);
            return result;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @id;";
            var param = new DynamicParameters();
            param.Add("@id", entity.GetId());

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);
            return result;
        }

        public async Task<int> DeleteManyAsync(List<TKey> ids)
        {
            var (entities, nonExistIds) = await GetManyAsync(ids);


            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id IN @ids;";
            var param = new DynamicParameters();

            param.Add("@ids", ids);

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);
           
            return result;
        }

        public async Task<int> InsertMultiAsync(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) return 0;

            // Lấy danh sách property của entity
            var props = entities[0].GetType().GetProperties();
            var param = new DynamicParameters();

            var columns = new List<string>();
            var paramsName = new List<string>();

            foreach (var prop in props)
            {
                var columnName = prop.Name;
                columns.Add(columnName);
            }

            for(int i = 0; i <  entities.Count; i++)
            {
                var paramName = new List<string>(); ;
                foreach(var prop in props)
                {
                    var propName = prop.Name;
                    var value = prop.GetValue(entities[i]);
                    param.Add($"@{propName + i}", prop.GetValue(entities[i]));
                    paramName.Add($"@{propName + i}");
                }
                paramsName.Add($"{string.Join(", ", paramName)}");
            }

            // Chuyển danh sách cột và tên của param thành chuỗi ngăn cách bởi dấu phẩy
            var columnsString = string.Join(", ", columns);
            var paramsNameString = string.Join(", ", paramsName.Select(item => $"({item})"));

            var sql = $"INSERT INTO {TableName} ({columnsString}) VALUES {paramsNameString};";

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction : Uow.Transaction);
            return result;
        }

        public async Task<int> UpdateMultiAsync(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0) return 0 ;

            var props = entities[0].GetType().GetProperties();
            var param = new DynamicParameters();

            var columns = new List<string>();
            var paramsName = new List<string>();
            var setColumns = new List<string>();

            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (propName == "Sources" || propName == "SourcesUpdates") continue;
                columns.Add(propName);
                setColumns.Add($"{propName} = VALUES({propName})");
            }

            for(int i = 0; i < entities.Count; i++)
            {
                var paramName = new List<string>();

                foreach(var prop in props)
                {
                    var propName = prop.Name;
                    if (propName == "Sources" || propName == "SourcesUpdates") continue;
                    param.Add($"@{propName + i}", prop.GetValue(entities[i]));
                    paramName.Add($"@{propName + i}");
                }
                paramsName.Add($"({string.Join(", ", paramName)})");
            }
            var columnsString = string.Join(", ", columns);
            var setColumnsString = string.Join(", ", setColumns);
            var paramsNameString = string.Join(", ", paramsName);

            var sql = $"INSERT INTO {TableName} ({columnsString}) VALUES {paramsNameString} " +
                $"ON DUPLICATE KEY UPDATE {setColumnsString};";

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);
            return result;
        }
    }
}
