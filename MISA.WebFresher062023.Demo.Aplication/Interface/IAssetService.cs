using MISA.WebFresher062023.Demo.Domain;
using OfficeOpenXml;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface IAssetService : ICrudService<Guid, AssetDto, AssetCreateDto, AssetUpdateDto, Asset>
    {
        /// <summary>
        /// hàm check trùng mã
        /// </summary>
        /// <param name="code">mã</param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<bool> IsDuplicateCodeAsync(string code);

        /// <summary>
        /// hàm lọc tài sản
        /// </summary>
        /// <param name="filterCreate"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<FilterAssetDto> FilterAssetAsync(FilterAssetCreateDto filterCreate);

        /// <summary>
        /// hàm xuất excel
        /// </summary>
        /// <param name="baseFilter">lọc theo điều kiện cơ bản</param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<ExcelPackage> ExportExcelAsync(FilterAsset baseFilter);

        /// <summary>
        /// Lấy chứng từ theo id tài sản
        /// </summary>
        /// <param name="assetId">id tài sản</param>
        /// <returns></returns>
        /// Createdby: ttnham(17/10/2023)
        Task<List<Increase>> GetIncreaseByAssetIdAsync(Guid assetId);

        /// <summary>
        /// lấy chưng từ theo danh sách id tài sản
        /// </summary>
        /// <param name="assetIds">danh sách id tài sản</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        Task<List<Guid>> GetIncreaseByListAssetIdsAsync(List<Guid> assetIds);
    }
}
