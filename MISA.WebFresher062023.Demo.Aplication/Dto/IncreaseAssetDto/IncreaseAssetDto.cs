namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseAssetDto
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
        /// mã loại tài sản
        /// </summary>
        public string AssetCategorycode { get; set; }

        /// <summary>
        /// tên loại tài sản
        /// </summary>
        public string AssetCategoryName { get; set; }

        /// <summary>
        /// thời gian sử dụng
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime UsedDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public float IncreaseCost { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public double DepreciationRate { get; set; }

        
        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public double ResidualValue
        {
            get
            {
                var timeUse = DateTime.Now.Year - UsedDate.Year;
                if (timeUse > LifeTime) return 0;
                else return Math.Floor(DepreciationRate * IncreaseCost * (LifeTime - timeUse));
            }
        }
        /// <summary>
        /// giá trị hao mòn
        /// </summary>
        public double AccumulatedDepreciation
        {
            get
            {
                return IncreaseCost - ResidualValue;
            }
        }
    }
}
