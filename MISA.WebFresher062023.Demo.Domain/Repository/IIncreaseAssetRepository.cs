namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IIncreaseAssetRepository : ICrudRepository<IncreaseAsset, Guid>
    {
        /// <summary>
        /// Hàm lấy các tài sản ghi tăng theo hứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        public Task<List<AssetIncreaseInfo>> GetListIncreaseDetailByIncreaseId(Guid increaseId);

        /// <summary>
        /// hàm xóa tài sản ghi tăng theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        public Task<int> DeleteAssetIncreaseByIncreaseId(Guid increaseId);


        /// <summary>
        /// Lấy danh sách ghi tăng tài sản theo Danh sách Id ghi tăng
        /// </summary>
        /// <param name="increaseIds">Danh sách Id ghi tăng</param>
        /// <returns>Danh sách ghi tăng - tài sản</returns>
        /// Created by: TTNham (24/09/2023)
        Task<List<IncreaseAsset>> GetIncreaseAssetByListIncreaseIds(List<Guid> increaseIds);

        /// <summary>
        /// lấy tài sản ghi tăng theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        Task<List<IncreaseAsset>> GetIncreaseAssetByIncreaesId(Guid increaseId);

        /// <summary>
        /// lấy tài sản ghi tăng theo id chứng từ và mã tài sản
        /// </summary>
        /// <param name="inceaseId"></param>
        /// <param name="assetIds"></param>
        /// <returns></returns>
        Task<List<IncreaseAsset>> GetIncreaseAssetByIncreaseIdAndAssetIds(Guid inceaseId, List<Guid> assetIds);
    }
}
