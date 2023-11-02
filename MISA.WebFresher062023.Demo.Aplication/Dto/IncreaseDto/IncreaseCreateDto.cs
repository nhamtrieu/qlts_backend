using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseCreateDto
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
        /// Ngày sửa đổi
        /// </summary>
        public DateTimeOffset? ModifyDate { get; set; }

        public string? Note { get; set; }

        /// <summary>
        /// danh sách tài sản update
        /// </summary>
        public List<AssetUpdateDto> AssetUpdateDtos { get; set; }

    }
}
