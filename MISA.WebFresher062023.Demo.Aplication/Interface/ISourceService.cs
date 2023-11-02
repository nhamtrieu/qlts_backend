using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface ISourceService : ICrudService<Guid, SourceDto, SourceCreateDto, SourceUpdateDto, Source>
    {
        /// <summary>
        /// lấy nguồn hình thành theo id tài sản
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        Task<List<SourceDto>> GetSourceByAssetIdAsync(Guid assetId);

        /// <summary>
        /// lấy nguồn hình thành theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        Task<List<SourceDto>> GetSourceByIncreaseIdAsync (Guid increaseId);

        /// <summary>
        /// xóa nguồn hình thành theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        Task<int> DeleteSourceByIncreaseIdAsync(Guid increaseId);

        /// <summary>
        /// xóa nguồn hinh thành theo id tài sản ghi tăng
        /// </summary>
        /// <param name="increaseAssetIds"></param>
        /// <returns></returns>
        Task<int> DeleteSourceByIncreaseAssetIds(List<Guid> increaseAssetIds);

        /// <summary>
        /// lấy nguồn hình thành theo id tài sản ghi tăng
        /// </summary>
        /// <param name="increaseAssetId"></param>
        /// <returns></returns>
        Task<List<SourceDto>> GetSourceByIncreaseAssetIdAsync(Guid increaseAssetId);
    }
}
