using System.Text.Json;

namespace MISA.WebFresher062023.Demo.Domain
{
    public class BaseException
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Thông tin trả về cho dev
        /// </summary>
        public string? DevMessage { get; set; }
        
        /// <summary>
        /// Thông tin trả về cho người dùng
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// Định danh theo dõi
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Lỗi
        /// </summary>
        public object? Errors { get; set; } 
        #endregion

        #region Methods
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        } 
        #endregion

    }
}
