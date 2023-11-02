using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;
using static Dapper.SqlMapper;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IUnitOfWork Uow;
        private readonly string TableName = "Token";
        public TokenRepository(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task<int> AddAsync(Token refreshToken)
        {
            var props = refreshToken.GetType().GetProperties();
            var param = new DynamicParameters();

            var columns = new List<string>();
            var paramNames = new List<string>(); 

            foreach(var prop in props)
            {
                var propName = prop.Name;
                param.Add($"{propName}", prop.GetValue(refreshToken));
                columns.Add(propName);
                paramNames.Add($"@{propName}");

            }

            var columnString = string.Join(", ", columns);
            var paramNameString = string.Join(", ", paramNames);

            var sql = $"INSERT INTO {TableName} ({columnString}) VALUES ({paramNameString})";

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);

            return result;
        }

        public Task<Token> GetByIdAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE RefreshTokenId = @id";
            var param = new DynamicParameters();
            param.Add("@id" , id);

            var result = Uow.Connection.QuerySingleOrDefaultAsync<Token>(sql, param, transaction:Uow.Transaction);

            return result;
        }

        public Task<Token> GetByUserIdAsync(Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE UserId = @id";
            var param = new DynamicParameters();
            param.Add("@id", userId);

            var result = Uow.Connection.QuerySingleOrDefaultAsync<Token>(sql, param, transaction: Uow.Transaction);

            return result;
        }

        public Task<Token> GetByTokenAsync(string token)
        {
            var sql = $"SELECT * FROM {TableName} WHERE RefreshToken = @token";
            var param = new DynamicParameters();
            param.Add("@token", token);

            var result = Uow.Connection.QuerySingleOrDefaultAsync<Token>(sql, param, transaction: Uow.Transaction);

            return result;
        }

        public async Task<int> RemoveAsync(Token refreshToken)
        {
            var sql = $"DELETE FROM {TableName} WHERE RefreshTokenId = @id";
            var param = new DynamicParameters();
            param.Add("@id", refreshToken.RefreshTokenId);

            var result = await Uow.Connection.ExecuteAsync(sql, param, Uow.Transaction);
            return result;
        }

        public async Task<int> UpdateAsync(Token refreshToken)
        {
            // Lấy danh sách property của entity
            var props = refreshToken.GetType().GetProperties();
            var param = new DynamicParameters();

            var paramKeyValue = new List<string>();
            foreach (var prop in props)
            {
                var propsName = prop.Name;
                var value = prop.GetValue(refreshToken);
                param.Add($"@{propsName}", value);
                paramKeyValue.Add($"{propsName} = @{propsName}");
                if (value == null) continue;
            }

            var paramKeyValueString = string.Join(", ", paramKeyValue);

            var sql = $"UPDATE {TableName} SET {paramKeyValueString} WHERE RefreshTokenId = @RefreshTokenId;";

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);
            return result;
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var sql = $"SELECT * FROM Users INNER JOIN Token ON Users.UserId = Token.UserId WHERE Token.RefreshToken = @refreshToken";

            var param = new DynamicParameters();

            param.Add("@refreshToken", refreshToken);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<User>(sql, param, transaction: Uow.Transaction);

            return result;

        }

        public async Task<Token> GetRefreshTokenByAccessTokenAsync(string accessToken)
        {
            var sql = "SELECT * FROM Token WHERE AccessToken = @accessToken;";
            var param = new DynamicParameters();
            param.Add("@accessToken", accessToken);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<Token>(sql, param, transaction: Uow.Transaction);
            return result;
        }
    }
}
