namespace MISA.WebFresher062023.Demo.Domain
{
    public class ValidateException : Exception
    {
        public int ErrorCode { get; set; }
        public ValidateException(string message) : base(message) { }

        public ValidateException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
