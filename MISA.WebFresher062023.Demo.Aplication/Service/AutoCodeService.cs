using MISA.WebFresher062023.Demo.Domain;
using MISA.WebFresher062023.Demo.Domain.Resource;
using System.Text.RegularExpressions;

namespace MISA.WebFresher062023.Demo.Application
{
    public class AutoCodeService : IAutoCodeService
    {

        private readonly IAutoCodeRepository _autoCodeRepository;

        public AutoCodeService(IAutoCodeRepository autoCodeRepository)
        {
            _autoCodeRepository = autoCodeRepository;
        }

        public async Task<string> GetNewCodeAsync(AutoCodeType codeType)
        {
            var maxCode = await _autoCodeRepository.GetMaxCodeAsync(codeType);

            if(maxCode == null)
            {
                throw new NotFiniteNumberException();
            } else
            {
                maxCode.Value++;
                return maxCode.Prefix + maxCode.Value.ToString().PadLeft(6, '0');
            }
        }

        public async Task<int> UpdateCodeAsync(AutoCodeType codeType, string newCode)
        {
            Match match = Regex.Match(newCode, @"([a-zA-Z]+)(\d+)");
            string prefix = match.Groups[1].Value;
            if (int.TryParse(match.Groups[2].Value, out var code))
            {
                var autoCode = new AutoCode
                {
                    Prefix = prefix,
                    Value = code,
                    CodeType = codeType
                };

                var result = await _autoCodeRepository.UpdateMaxCodeAsync(autoCode);
                return result;
            } else
            {
                throw new ValidateException(Resource.InvalidCode);
            }
        }
    }
}
