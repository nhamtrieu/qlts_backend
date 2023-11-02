namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IAssetRepository : ICrudRepository<Asset, Guid>
    {
        /// <summary>
        /// Tìm kiếm tài sản theo mã
        /// </summary>
        /// <param name="code">mã tài sản</param>
        /// <returns>Tài sản</returns>
        /// CreatedBy: ttnham (18/08/2023)
        Task<Asset?> FindByCodeAsync(string code);

        /// <summary>
        /// lọc tài sản theo điều kiện lọc
        /// </summary>
        /// <param name="filterAsset"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<FilterAsset> FilterAssetAsync(FilterAsset filterAsset, List<Guid> ids);

        /// <summary>
        /// lọc theo điều kiện lọc cơ bản
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        Task<List<Asset>> FilterBaseAsync(FilterAsset baseFilter);

        /// <summary>
        /// lấy chứng từ theo id tài sản
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        Task<List<Increase>> GetIncreaseByAssetIdAsync(Guid assetId);

        /// <summary>
        ///  lấy id tài sản ghi tăng theo danh sách id tài sản
        /// </summary>
        /// <param name="assetIds"></param>
        /// <returns></returns>
        /// Createdby: ttnham (17/10/2023)
        Task<List<Guid>> GetIncreaseByListAssetIdsAsync(List<Guid> assetIds);
    }
}
