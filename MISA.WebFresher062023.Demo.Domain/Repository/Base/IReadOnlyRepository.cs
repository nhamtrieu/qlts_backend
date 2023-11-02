namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IReadOnlyRepository<TEntity, Tkey> where TEntity : IEntity<Tkey>
    {
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: ttnham (15/08/2023)
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: ttnham (15/08/2023)
        Task<TEntity> GetAsync(Tkey id);

        /// <summary>
        /// Lấy nhiều bản ghi theo id
        /// </summary>
        /// <param name="ids">Danh sách ID</param>
        /// <returns>Danh sách bản ghi</returns>
        Task<(List<TEntity>, List<Tkey>)> GetManyAsync(List<Tkey> ids);

        /// <summary>
        /// Tìm kiếm bản ghi theo id
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: ttnham (15/08/2023)
        Task<TEntity> FindAsync(Tkey id);

        /// <summary>
        /// Lấy bản ghi được tạo mới gần nhất
        /// </summary>
        /// <returns>Bản ghi</returns>
        /// Createdby: ttnham (15/08/2023)
        Task<TEntity> GetLastEntityAsync();


        /// <summary>
        /// lấy bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetByListIdAsync(List<Tkey> ids);
    }
}
