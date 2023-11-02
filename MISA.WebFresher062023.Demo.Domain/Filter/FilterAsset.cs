namespace MISA.WebFresher062023.Demo.Domain
{
    public class FilterAsset : BaseFilter<Asset>
    {
        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// tên loại tài sản
        /// </summary>
        public string? AssetCategoryName { get; set; }

    }
}
