using MISA.WebFresher062023.Demo.Domain;
using Microsoft.AspNetCore.Http;

namespace MISA.WebFresher062023.Demo.Application
{
    public abstract class BaseCrudService<TEntity, TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyService<TEntity, TKey, TEntityDto>, ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class where TEntity : IEntity<TKey>
    {

        protected  readonly ICrudRepository<TEntity, TKey> CrudRepository;
        protected BaseCrudService(ICrudRepository<TEntity, TKey> repository) : base(repository)
        {
            CrudRepository = repository;
        }
        

        public virtual async Task<int> DeleteAsync(TKey id)
        {
            var entity = await CrudRepository.GetAsync(id);
            var result = await CrudRepository.DeleteAsync(entity);
            return result;
        }

        public virtual async Task<int> DeleteManyAsync(List<TKey> ids)
        {
            var entities = new List<TEntity>();
            var listIds = new List<TKey>();
            (entities, listIds) = await CrudRepository.GetManyAsync(ids);
            if(entities.Count == ids.Count)
            {
                var result = await CrudRepository.DeleteManyAsync(ids);
                return result;
            } else
            {
                throw new NotFoundException();
            }
        }

        public virtual async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = await MapEntityCreateDtoToEntity(entityCreateDto);
            
            var result = await CrudRepository.InsertAsync(entity);
            return result;
        }
        
        public virtual async Task<int> UpdateAsync(TKey id, TEntityUpdateDto entityUpdateDto)
        {
            var entity = await CrudRepository.FindAsync(id);
            if (entity == null) throw new NotFoundException("Không tìm thấy tài nguyên");
            entity = await MapEntityUpdateDtoToEntity(id,entityUpdateDto);
            await CrudRepository.UpdateAsync(entity);
            var entityDto = MapEntityToEntityDto(entity);
            return 1;
        }

        public async Task<int> InsertMultiAsync(List<TEntity> entities)
        {
            var result = await CrudRepository.InsertMultiAsync(entities);

            return result;
        }

        public async Task<List<TEntityDto>> UpdateMultiAsync(List<TEntity> entities)
        {
            var entitiesUpdate = entities;
           
            await CrudRepository.UpdateMultiAsync(entitiesUpdate);
            var entitiesDto = entitiesUpdate.Select( entity =>  MapEntityToEntityDto(entity)).ToList(); 

            return entitiesDto;
        }



        public abstract Task<TEntity> MapEntityCreateDtoToEntity(TEntityCreateDto createDto);

        public abstract Task<TEntity> MapEntityUpdateDtoToEntity(TKey id,TEntityUpdateDto updateDto);
    }
}
