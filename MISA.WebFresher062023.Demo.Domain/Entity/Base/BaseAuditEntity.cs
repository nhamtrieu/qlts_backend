namespace MISA.WebFresher062023.Demo.Domain
{
    public abstract class BaseAuditEntity
    {
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; } 

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
