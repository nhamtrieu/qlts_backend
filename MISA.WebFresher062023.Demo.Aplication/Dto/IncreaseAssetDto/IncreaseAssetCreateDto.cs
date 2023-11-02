namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseAssetCreateDto
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid AssetDetailID { get; set; }

        /// <summary>
        /// ID chứng từ
        /// </summary>
        public Guid IncreaseId { get; set; }

        /// <summary>
        /// id tài sản
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Giá ghi tăng
        /// </summary>
        public float IncreaseCost { get; set; }
    }
}
