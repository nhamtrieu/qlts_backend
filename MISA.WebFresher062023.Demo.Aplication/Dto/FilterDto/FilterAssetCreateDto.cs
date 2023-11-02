namespace MISA.WebFresher062023.Demo.Application
{
    public class FilterAssetCreateDto
    {
        /// <summary>
        /// trang hiện tại
        /// </summary>
        public int PageNumber { get; set; } 
        /// <summary>
        /// kích thước trang
        /// </summary>
        public int PageSize { get; set; } 

        /// <summary>
        /// chuỗi tìm kiếm
        /// </summary>
        public string? SearchString { get; set; }

        /// <summary>
        /// tên bộ phận
        /// </summary>
        public string? DepartmentName { get; set; } = null;

        /// <summary>
        /// tên loại tài sản
        /// </summary>
        public string? AssetCategoryName { get; set; } = null;

        /// <summary>
        /// list id tài sản
        /// </summary>
        public List<Guid>? assetid { get; set; }
    }
}
