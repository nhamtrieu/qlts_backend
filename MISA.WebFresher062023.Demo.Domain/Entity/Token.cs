namespace MISA.WebFresher062023.Demo.Domain
{
    public class Token
    {
        /// <summary>
        /// id refresh token
        /// </summary>
        public Guid RefreshTokenId { get; set; }

        /// <summary>
        /// refresh token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// ngày hết hạn token
        /// </summary>
        public DateTimeOffset RefreshTokenExpiresAt { get; set; }
    }
}
