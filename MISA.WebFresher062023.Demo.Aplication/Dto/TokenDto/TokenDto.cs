namespace MISA.WebFresher062023.Demo.Application
{
    public class TokenDto
    {
        /// <summary>
        /// refresh token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// ngày hết hạn refresh token
        /// </summary>
        public DateTimeOffset RefreshTokenExpiresAt { get; set; }
    }
}
