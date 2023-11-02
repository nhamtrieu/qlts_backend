namespace MISA.WebFresher062023.Demo.Application
{
    public class BaseFilterDto<TEntityDto>
    {
        /// <summary>
        /// So trang hien tai
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Số bản ghi trên 1 trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public List<TEntityDto>? Data { get; set; }
    }
}
