namespace MISA.WebFresher062023.Demo.Domain
{
    public class IncreaseManager : EntityManager<Increase, Guid>, IIncreaseManger
    {

        private readonly IIncreaseRepository _increaseRepository;

        public IncreaseManager(IIncreaseRepository increaseRepository) : base(increaseRepository)
        {
            _increaseRepository = increaseRepository;
        }

        public async Task CheckDuplicateCodeAsync(string code, Guid? id = null)
        {
            var increase = await _increaseRepository.FindByCodeAsync(code);

            if (increase != null && increase.IncreaseId != id)
            {
                throw new ConflictException(Resource.Resource.DuplicateIncreaseCode, 409);
            }
        }

        public void CheckValidIncreaseCode(string code)
        {
            if (!char.IsDigit(code[code.Length - 1]))
            {
                throw new ValidateException(Resource.Resource.IncreaseCodeNotEndNumber);
            }
        }
    }
}
