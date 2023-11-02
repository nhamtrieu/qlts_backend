namespace MISA.WebFresher062023.Demo.Domain
{
    public class RefreshToken
    {
        /// <summary>
        /// ID người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// ID của refresh token
        /// </summary>
        public Guid RefreshTokenId { get; set; }

        /// <summary>
        /// refresh token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// thời gian hết hạn
        /// </summary>
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
