namespace MISA.WebFresher062023.Demo.Application
{
    public class TokenReturnDto
    {

        /// <summary>
        /// access token
        /// </summary>
        public string accessToken { get; set; }

        /// <summary>
        /// ngày hết hạn token
        /// </summary>
        public DateTimeOffset tokenExpiration { get; set; }
    }
}
