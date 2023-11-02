using MISA.WebFresher062023.Demo.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MISA.WebFresher062023.Demo.Application
{
    public class AssetCreateDto
    {
        [Required(ErrorMessage ="Mã tài sản không được phép để trống!")]
        [MaxLength(10, ErrorMessage ="Mã tài sản tối đa 10 ký tự")]
        /// <summary>
        /// Mã tải sản
        /// </summary>
        public string AssetCode { get; set; }

        [Required(ErrorMessage ="Tên tài sản không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Mã tài sản tối đa 255 ký tự")]
        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = "Mã phòng ban không được phép để trống")]
        [MaxLength(10, ErrorMessage = "Mã phòng ban tối đa 10 ký tự")]
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Tên phòng ban không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên phòng ban tối đa 255 ký tự")]
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        
        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid AssetCategoryId { get; set; }

        [Required(ErrorMessage = "Mã loại tài sản không được phép để trống")]
        [MaxLength(10, ErrorMessage = "Mã loại tài sản tối đa 10 ký tự")]
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string AssetCategoryCode { get; set; }

        [Required(ErrorMessage = "Tên phòng ban không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên phòng ban tối đa 255 ký tự")]
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetCategoryName { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTimeOffset PurchaseDate { get; set; }

        /// <summary>
        /// Giá
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int TrackedYear { get; set; }

        /// <summary>
        /// Thời gian sử dụng
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Năm sản xuất
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }

        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        public DateTimeOffset UsedDate { get; set; }
    }
}

