namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreasePutDto 
    {
        /// <summary>
        /// Thông tin chứng từ
        /// </summary>
        public IncreaseUpdateDto? IncreaseUpdateDto { get; set; }

        /// <summary>
        /// Danh sách tài sản được cập nhật
        /// </summary>
        public List<AssetUpdateDto>? AssetsUpdateDto { get; set; }

        /// <summary>
        /// Danh sách tài sản cần tạo mới
        /// </summary>
        public List<AssetUpdateDto>? AssetsCreateDto { get; set; }

        /// <summary>
        /// Danh sách id tài sản xóa
        /// </summary>
        public List<Guid>? AssetDeleteIds { get; set; }

        /// <summary>
        /// Danh sách tài sản ghi tăng cần xóa
        /// </summary>
        public List<Guid>? IncreaseAssetDeleteIds { get; set; }

        /// <summary>
        ///  danh sách id nguồn hình thành xóa
        /// </summary>
        public List<Guid>? SourceDeteleIds { get; set; }
    }
}
