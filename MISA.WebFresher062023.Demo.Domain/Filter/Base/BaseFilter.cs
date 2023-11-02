namespace MISA.WebFresher062023.Demo.Domain {
    public class BaseFilter<TEntity>
    {
        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Số bản ghi 1 trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public List<TEntity>? Data { get; set; }

        /// <summary>
        /// chuỗi tìm kiếm
        /// </summary>
        public string? SearchString { get; set; }
    }
}
