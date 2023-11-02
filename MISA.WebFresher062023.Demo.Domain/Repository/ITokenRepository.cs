namespace MISA.WebFresher062023.Demo.Domain
{
    public interface ITokenRepository
    {
        /// <summary>
        /// Thêm thông tin refresh token vào db
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<int> AddAsync(Token token);

        /// <summary>
        /// Lấy ra refreshtoken theo refreshtoken id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Token> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Lấy ra refreshtoken theo userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Token> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Lấy ra refreshtoken theo refreshtoken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Token> GetByTokenAsync(string token);

        /// <summary>
        /// Lấy thông tin người dùng theo refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// cập nhật refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Token refreshToken);

        /// <summary>
        /// xóa refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(Token refreshToken);

        /// <summary>
        /// lấy refresh token theo assettoken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<Token> GetRefreshTokenByAccessTokenAsync(string accessToken);
    }
}
