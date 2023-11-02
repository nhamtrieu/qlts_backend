using Dapper;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public class AutoCodeRepository : IAutoCodeRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public AutoCodeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<AutoCode> GetMaxCodeAsync(AutoCodeType codeType)
        {
            string sql = $"SELECT * FROM AutoCode WHERE Type = {(int)codeType}";

            var result =  await _unitOfWork.Connection.QueryFirstOrDefaultAsync<AutoCode>(sql, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<int> UpdateMaxCodeAsync(AutoCode code)
        {
            string sql = $"UPDATE AutoCode " +
                $"SET Prefix = @Prefix , Value = @Value " +
                $"WHERE Type = @Type";

            var param = new DynamicParameters();
            param.Add("@Prefix", code.Prefix);
            param.Add("@Value", code.Value);
            param.Add("@Type", code.CodeType);

            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, transaction: _unitOfWork.Transaction);

            return result;
        }
    }
}
