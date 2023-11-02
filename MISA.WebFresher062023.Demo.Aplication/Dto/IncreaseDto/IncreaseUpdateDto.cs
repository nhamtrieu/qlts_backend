using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseUpdateDto
    {
        [Required(ErrorMessage = "Mã chứng từ không được phép để trống")]
        public string IncreaseCode { get; set; }

        [Required(ErrorMessage = "Ngày chứng từ không được phép để trống")]
        /// <summary>
        /// Ngày chứng từ 
        /// </summary>
        public DateTimeOffset IncreaseDate { get; set; }

        [Required(ErrorMessage = "Ngày ghi tăng không được phép để trống")]
        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTimeOffset IncreaseRecordDate { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTimeOffset? ModifyDate { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
    }
}
