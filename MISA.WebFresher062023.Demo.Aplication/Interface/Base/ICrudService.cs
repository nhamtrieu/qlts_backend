using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> : IReadOnlyService<TKey, TEntityDto> where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi</param>
        /// <returns></returns>
        /// CreatedBy: ttnham (18/08/2023)
        Task<int> InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Hàm sửa 1 bản ghi
        /// </summary>
        /// <param name="id">id tài sản</param>
        /// <param name="entityUpdateDto">tài sản</param>
        /// <returns></returns>
        /// CreatedBy: ttnham (18/08/2023)
        Task<int> UpdateAsync(TKey id, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Hàm xóa 1 tài sản
        /// </summary>
        /// <param name="id"> Id tài sản</param>
        /// <returns>tài sản bị xóa</returns>
        /// CreatedBy: ttnham (18/08/2023)
        Task<int> DeleteAsync(TKey id);

        /// <summary> 
        /// Hàm xóa nhiều tài sản
        /// </summary>
        /// <param name="ids">List id tài sản</param>
        /// <returns>tài sản bị xóa</returns>
        /// CreatedBy: ttnham (18/08/2023)
        Task<int> DeleteManyAsync(List<TKey> ids);

        /// <summary>
        /// thêm nhiều bản ghi
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> InsertMultiAsync(List<TEntity> entities);

        /// <summary>
        /// sửa nhiều bản ghi
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<List<TEntityDto>> UpdateMultiAsync(List<TEntity> entities);
    }
}
