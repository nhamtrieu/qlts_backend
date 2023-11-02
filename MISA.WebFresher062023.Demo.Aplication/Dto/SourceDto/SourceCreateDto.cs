using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class SourceCreateDto
    {
        /// <summary>
        /// Nguôn hình thành
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        public float Cost { get; set; }

        /// <summary>
        /// id tài sản ghi tăng
        /// </summary>
        public Guid IncreaseAssetId { get; set; }
    }
}
