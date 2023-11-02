using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class SourceDto
    {
        /// <summary>
        /// nguồn hình thành
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// nguyên giá
        /// </summary>
        public float Cost { get; set; }

        /// <summary>
        /// id tài sản ghi tăng
        /// </summary>
        public Guid IncreaseAssetId { get; set; }

        /// <summary>
        /// id nguồn hình thành
        /// </summary>
        public Guid SourceId { get; set; }
    }
}
