using MISA.WebFresher062023.Demo.Domain;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher062023.Demo.Application
{
    public class DepartmentDto
    {
        [Required]
        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTimeOffset ModifyDate { get; set; }
    }
}
