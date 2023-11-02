namespace MISA.WebFresher062023.Demo.Domain
{
    public class ConflictException : Exception
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        public ConflictException() { }
        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
