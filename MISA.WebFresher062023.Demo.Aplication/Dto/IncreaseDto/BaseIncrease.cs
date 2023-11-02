namespace MISA.WebFresher062023.Demo.Application
{
    public class BaseIncrease
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string IncreaseCode { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTimeOffset IncreaseDate { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTimeOffset IncreaseRecordDate { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }
    }
}
