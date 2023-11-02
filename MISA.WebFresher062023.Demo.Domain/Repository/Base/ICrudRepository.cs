namespace MISA.WebFresher062023.Demo.Domain
{
    public interface ICrudRepository<TEntity, Tkey> : IReadOnlyRepository<TEntity, Tkey> where TEntity : IEntity<Tkey>
    {

        /// <summary>
        /// Tên bản ghi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: ttnham (15/08/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: ttnham (15/08/2023)
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: ttnham (15/08/2023)
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// CreatedBy: ttnham (15/08/2023)
        Task<int> DeleteManyAsync(List<Tkey> tkeys);

        /// <summary>
        /// thêm nhiều bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertMultiAsync(List<TEntity> entities);

        /// <summary>
        /// sửa nhiều bản ghi
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UpdateMultiAsync(List<TEntity> entities);
    } 
}
