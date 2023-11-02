namespace MISA.WebFresher062023.Demo.Application
{
    public class UserReturnDto
    {
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// ngày hết hạn token
        /// </summary>
        public DateTimeOffset tokenExpiration { get; set; }
    }
}
