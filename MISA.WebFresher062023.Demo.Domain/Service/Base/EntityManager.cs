namespace MISA.WebFresher062023.Demo.Domain
{
    public class EntityManager<TEntity, TKey> : IEntityManager<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        private readonly ICrudRepository<TEntity, TKey> _crudRepository;

        public EntityManager(ICrudRepository<TEntity, TKey> crudRepository)
        {
            _crudRepository = crudRepository;
        }

        public async Task<List<TEntity>> GetByListIdAsync(List<TKey> ids)
        {
            var entities = await _crudRepository.GetByListIdAsync(ids);

            var entityIds = entities.Select(entity => entity.GetId()).ToList();

            var idsNotExist = new List<string>();

            ids.ForEach(id =>
            {
                if (!entityIds.Contains(id))
                {
                    idsNotExist.Add(id.ToString());
                }
            });

            if(idsNotExist.Count > 0)
            {
                throw new NotFoundException(string.Join(", ", idsNotExist) + " " + typeof(TEntity).FullName);
            }

            return entities;
        }
    }
}
