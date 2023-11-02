namespace MISA.WebFresher062023.Demo.Application
{
    public class AssetUpdateDto
    {

        public Guid? AssetId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        /// 
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã bộ phận sử dụng
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid AssetCategoryId { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetCategoryName { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string AssetCategoryCode { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTimeOffset PurchaseDate { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public float Cost { get; set; }

        public float IncreaseCost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public double DepreciationRate { get; set; }

        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int TrackedYear { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }

        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        public DateTimeOffset UsedDate { get; set; }

        /// <summary>
        /// danh sách nguồn hình thành
        /// </summary>
        public List<SourceCreateDto>? Sources { get; set; }

        /// <summary>
        /// danh sách nguồn hình thành
        /// </summary>
        public List<SourceUpdateDto>? SourcesUpdates { get; set; }

        /// <summary>
        /// Id ghi tăng - tài sản
        /// </summary>
        public Guid? IncreaseAssetId { get; set; }
    }
}
