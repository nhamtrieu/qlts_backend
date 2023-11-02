namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseDto
    {
        /// <summary>
        /// ID chứng từ
        /// </summary>
        public Guid IncreaseId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        public string IncreaseCode { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTimeOffset IncreaseDate { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTimeOffset IncreaseRecordDate { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Người sửa dổi
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// danh sách ghi tăng - tài sản
        /// </summary>
        public List<IncreaseAssetDto> IncreaseAsset { get; set; }

        /// <summary>
        /// tổng nguyên giá
        /// </summary>
        public double TotalCost{
            get
            {
                return IncreaseAsset.Sum(x => x.IncreaseCost);
            }
        }
    }
}
