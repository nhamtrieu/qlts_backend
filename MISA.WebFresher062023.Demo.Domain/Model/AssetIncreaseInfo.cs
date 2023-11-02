namespace MISA.WebFresher062023.Demo.Domain
{
    public class AssetIncreaseInfo
    {
        /// <summary>
        /// Id tài sản ghi tăng
        /// </summary>
        public Guid IncreaseAssetId { get; set; }

        /// <summary>
        /// Id của chứng từ
        /// </summary>
        public Guid IncreaseId { get; set; }

        /// <summary>
        /// Id của tài sản
        /// </summary>
        public Guid AssetId { get; set; }

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
        /// Mã bộ phận sử dụng
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>

        public Guid AssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>

        public string AssetCategoryCode { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>

        public string AssetCategoryName { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime UsedDate { get; set; }
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

        public float AssetCost { get; set; }
    }

}
