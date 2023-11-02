namespace MISA.WebFresher062023.Demo.Domain
{
    public class Asset : IEntity<Guid>
    {
        /// <summary>
        /// ID của tải sản
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Mã của tài sản
        /// </summary>
        public string? AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? AssetName { get; set; }


        /// <summary>
        /// ID của phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// ID loại tài sản cố định
        /// </summary>
        public Guid AssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản cố định
        /// </summary>
        public string? AssetCategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetCategoryName { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTimeOffset PurchaseDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int TrackedYear { get; set; }

        /// <summary>
        /// Thời gian sử dụng
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Năm sản xuất
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }

        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        public DateTimeOffset UsedDate { get; set; }

        /// <summary>
        /// nguòn hình thành
        /// </summary>
        //public List<Source>? Sources { get; set; }

        public Guid GetId()
        {
            return AssetId;
        }

        public string GetCode()
        {
            return AssetCode;
        }

        public void SetId(Guid id)
        {
            AssetId = id;
        }

        public void SetCode(string code)
        {
            AssetCode = code;
        }
    }
}

