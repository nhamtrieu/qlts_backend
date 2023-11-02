namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreasePostDto
    {
        /// <summary>
        ///  chứng từ ghi tăng
        /// </summary>
        public IncreaseCreateDto IncreaseCreateDto { get; set; }

        /// <summary>
        /// danh sách tài sản thuộc chứng từ
        /// </summary>
        public List<AssetUpdateDto> AssetsUpdateDto { get; set; }
    }
}
