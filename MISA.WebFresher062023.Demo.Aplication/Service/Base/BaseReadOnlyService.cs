using MISA.WebFresher062023.Demo.Domain;
using System.Text.RegularExpressions;

namespace MISA.WebFresher062023.Demo.Application
{
    public abstract class BaseReadOnlyService<TEntity, TKey, TEntityDto> : IReadOnlyService<TKey, TEntityDto> where TEntityDto : class where TEntity : IEntity<TKey>
    {
        protected readonly IReadOnlyRepository<TEntity, TKey> ReadOnlyRepository;
        public BaseReadOnlyService(IReadOnlyRepository<TEntity, TKey> repository)
        {
            ReadOnlyRepository = repository;
        }

        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await ReadOnlyRepository.GetAllAsync();
            var result = entities.Select(entity => MapEntityToEntityDto(entity)).ToList();
            return result;
        }

        public async Task<TEntityDto> GetAsync(TKey id)
        {
            var entity = await ReadOnlyRepository.GetAsync(id);
            var result =  MapEntityToEntityDto(entity);
            return result;
        }

        public async Task<List<TEntityDto>> GetManyAsync(List<TKey> ids)
        {
            var listIds = new List<TKey>();
            var  entities = new List<TEntity>();
            (entities, listIds) = await ReadOnlyRepository.GetManyAsync(ids);
            var entityDtos = entities.Select(entity => MapEntityToEntityDto(entity)).ToList();
            return entityDtos;
        }

        public async Task<string> GetNewCode(string prefix)
        {
            var entity = await ReadOnlyRepository.GetLastEntityAsync();
            if (entity == null) return $"{prefix}0001";
            
                string input = entity.GetCode();
                string pattern = @"(.+\D)(\d+)$";

                Match match = Regex.Match(input, pattern);
                string letters = match.Groups[1].Value; // Chuỗi chữ và số cuối cùng
                string numbers = match.Groups[2].Value; // Chuỗi số cuối cùng

                string numbersAfter =  IncrementNumberString(numbers);

                return $"{letters}{numbersAfter}";
            
        }

        public abstract TEntityDto MapEntityToEntityDto(TEntity entity);

        public abstract string IncrementNumberString(string numberString);

    }
}
