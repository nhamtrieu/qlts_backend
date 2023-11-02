using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface IIncreaseAssetService : ICrudService<Guid, IncreaseAssetDto, IncreaseAssetCreateDto, IncreaseAssetUpdateDto,IncreaseAsset>
    {
        /// <summary>
        /// lẩy danh sách tài sản ghi tăng theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        Task<List<IncreaseAssetDto>> GetIncreaseAssetByIncreaseId(Guid increaseId);

        /// <summary>
        /// lấy toàn bộ tài sản ghi tăng theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        Task<List<IncreaseAssetDto>> GetAllIncreaseAssetByIncreaseIdAsync(Guid increaseId);

        /// <summary>
        /// Hàm xóa tài sản ghi tăng theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        /// Createdby: ttnham (06/09/2023)
        Task<int> DeleteIncreaseAssetByIncreaseIdAsync(Guid increaseId);

        /// <summary>
        /// lấy danh sách tài sản ghi tăng theo danh sách id chứng từ
        /// </summary>
        /// <param name="increaseIds"></param>
        /// <returns></returns>
        Task<List<IncreaseAssetDto>> GetIncreaseAssetByListIncreaseIds(List<Guid> increaseIds);

        /// <summary>
        /// lấy danh sách tài sản ghi tăng theo id chứng từ và tài sản
        /// </summary>
        /// <param name="inceaseId"></param>
        /// <param name="assetIds"></param>
        /// <returns></returns>
        Task<List<IncreaseAsset>> GetIncreaseAssetByIncreaseIdAndAssetIds(Guid inceaseId, List<Guid> assetIds);
    }
}
