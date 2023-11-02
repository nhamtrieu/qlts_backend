namespace MISA.WebFresher062023.Demo.Domain
{
    public class IncreaseJoin : BaseAuditEntity, IEntity<Guid>
    {
        /// <summary>
        /// id chứng từ
        /// </summary>
        public Guid IncreaseId { get; set; }

        /// <summary>
        /// mã chứng từ
        /// </summary>
        public string IncreaseCode { get; set; }

        /// <summary>
        /// ngày chứng từ
        /// </summary>
        public DateTimeOffset IncreaseDate { get; set; }

        /// <summary>
        /// ngày ghi tăng
        /// </summary>
        public DateTimeOffset IncreaseRecordDate { get; set; }

        /// <summary>
        /// tổng nguyên giá
        /// </summary>
        public float? TotalCost { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string? Description { get; set; }

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
