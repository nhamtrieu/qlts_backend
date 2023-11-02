
namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IIncreaseRepository : ICrudRepository<Increase, Guid>
    {
        /// <summary>
        /// lấy chứng từ theo id tài sản ghi tăng
        /// </summary>
        /// <param name="increasesId"></param>
        /// <returns></returns>
        Task<List<Increase>> GetIncreasesByIncreaseAssetIdAsync(Guid increasesId);

        /// <summary>
        /// lấy mã code lớn nhất
        /// </summary>
        /// <returns></returns>
        Task<string> GetMaxIncreaseCodeAsync();
        Task<Tuple<List<IncreaseJoin>, int>> GetPaginIncreaseAsync(int limit, int offset);
        Task<Tuple<List<IncreaseJoin>, int>> SearchIncreaseAsync(string key, int limit, int offset);

        /// <summary>
        /// lọc chứng từ
        /// </summary>
        /// <param name="filterIncrease"></param>
        /// <returns></returns>
        Task<FilterIncrease> FilterIncreaseAysnc(FilterIncrease filterIncrease);
        
        /// <summary>
        /// Tìm kiếm chứng từ theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<Increase?> FindByCodeAsync(string code);
    }
}
