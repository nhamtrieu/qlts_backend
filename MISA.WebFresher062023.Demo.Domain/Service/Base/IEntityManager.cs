namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IEntityManager<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<List<TEntity>> GetByListIdAsync(List<TKey> id);
    }
}
