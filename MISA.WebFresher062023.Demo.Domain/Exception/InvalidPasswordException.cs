namespace MISA.WebFresher062023.Demo.Domain
{
    public class InvalidPasswordException : Exception
    {
        public int ErrorCode { get; set; }
        public InvalidPasswordException() { }

        public InvalidPasswordException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public InvalidPasswordException(string message) : base(message) 
        {

        }
    }
}
