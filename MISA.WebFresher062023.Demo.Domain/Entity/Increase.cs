using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher062023.Demo.Domain
{
    public class Increase : IEntity<Guid>
    {
        /// <summary>
        /// ID chứng từ
        /// </summary>
        public Guid IncreaseId { get; set; }

        [Required(ErrorMessage ="Mã chứng từ không được phép để trống")]
        /// <summary>
        /// Mã chứng từ
        /// </summary>
        public string? IncreaseCode { get; set; }

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
        /// Người sửa dổi
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }

        /// <summary>
        /// ghi chu
        /// </summary>
        public string? Note { get; set; }

        public string GetCode()
        {
            return IncreaseCode;
        }

        public Guid GetId()
        {
            return IncreaseId;
        }

        public void SetCode(string code)
        {
            IncreaseCode = code;
        }

        public void SetId(Guid id)
        {
            IncreaseId = id;
        }
    }
}
