namespace MISA.WebFresher062023.Demo.Application
{
    public class AssetCategoryDto
    {
        /// <summary>
        /// ID của danh mục tài sản cố định.
        /// </summary>
        public Guid AssetCategoryId { get; set; }

        /// <summary>
        /// Mã danh mục tài sản cố định.
        /// </summary>
        public string? AssetCategoryCode { get; set; }

        /// <summary>
        /// Tên danh mục tài sản cố định.
        /// </summary>
        public string? AssetCategoryName { get; set; }

        /// <summary>
        /// Thời gian sử dụng ước tính (tuổi thọ).
        /// </summary>

        public int LifeTime { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }
        }
    }

