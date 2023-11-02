namespace MISA.WebFresher062023.Demo.Domain
{
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        public NotFoundException() { }
        public NotFoundException(int errorCode) 
        {
            ErrorCode = errorCode;
        }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, int errorCode) : base(message) 
        {
            ErrorCode = errorCode;
        }
    }
}
