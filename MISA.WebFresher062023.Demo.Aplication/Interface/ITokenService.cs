using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface ITokenService
    {
        /// <summary>
        /// hàm  refresh access token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<TokenReturnDto> RefreshTokenAsync(string accessToken);

        /// <summary>
        /// hàm xóa refresh token ra khỏi db
        /// </summary>
        /// <param name="assetToken"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<int> RemoveRefreshTokenAsync(string assetToken);

        /// <summary>
        /// hàm sinh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        string GenerateJwtToken(Token token);
    }
}
