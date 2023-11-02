namespace MISA.WebFresher062023.Demo.Domain
{
    public interface ISourceRepository : ICrudRepository<Source, Guid>
    {
        /// <summary>
        /// lây nguồn hình thành theo id tài sản
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        Task<List<Source>> GetSourcetByAssetIdAsync(Guid assetId);

        /// <summary>
        /// lấy nguồn hình thành theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        Task<List<Source>> GetSourceByIncreaseIdAsync(Guid increaseId);

        /// <summary>
        /// xóa nguồn hình thành theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        Task<int> DeleteSourceByIncreaseIdAsync(Guid increaseId);

        /// <summary>
        /// xóa nguồn hình thành theo danh sách id ghi tăng tài sản
        /// </summary>
        /// <param name="assetIncreaseIds"></param>
        /// <returns></returns>
        Task<int> DeleteSourceByIncreaseAssetIds(List<Guid> assetIncreaseIds);

        /// <summary>
        /// Lấy danh sách nguồn hình thành theo id tài sản ghi tăng
        /// </summary>
        /// <param name="increaseAssetId"></param>
        /// <returns></returns>
        Task<List<Source>> GetSourcesByIncreaseAssetIdAsync(Guid increaseAssetId);

        /// <summary>
        /// Xóa nguồn hình thanh theo id ghi tăng tài sản
        /// </summary>
        /// <param name="increaseAssetId"></param>
        /// <returns></returns>
        Task<int> DeleteSourceByIncreaseAssetId(Guid increaseAssetId);
    }
}
