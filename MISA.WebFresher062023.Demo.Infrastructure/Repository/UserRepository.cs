using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class UserRepository : BaseReadOnlyRepository<User, Guid>, IUserRepository
    {
        public new string TableName = "User";
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }
        public async Task<User> CheckUserInfo(User user)
        {
            var sql = $"SELECT * FROM {TableName} WHERE UserName = @username OR Email = @username OR PhoneNumber = @username";

            var param = new DynamicParameters();
            param.Add("@username", user.UserName);   

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<User>(sql, param, transaction:Uow.Transaction);
            return result;
        }

        public async Task<User> FindByUserName(string username)
        {
            var sql = $"SELECT * FROM {TableName} WHERE UserName = @username";

            var param = new DynamicParameters();
            param.Add("@username", username);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<User>(sql, param, transaction: Uow.Transaction);
            return result;
        }
    }
}
