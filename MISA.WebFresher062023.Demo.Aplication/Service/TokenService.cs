using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.WebFresher062023.Demo.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MISA.WebFresher062023.Demo.Application
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public TokenService(ITokenRepository refreshTokenRepository, IUserRepository userRepository, IMapper mapper, IUserService userService, IConfiguration config)
        {
            _tokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _config = config;
        }

        public string GenerateJwtToken(Token token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim("RefreshTokenID", token.RefreshTokenId.ToString()),
                new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
            };

            var tokenResult = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"])),
                signingCredentials: credentials
            );

            var newToken = new JwtSecurityTokenHandler().WriteToken(tokenResult);

            return newToken;
        }

        public async Task<TokenReturnDto> RefreshTokenAsync(string accessToken)
        {

            var token = await _tokenRepository.GetRefreshTokenByAccessTokenAsync(accessToken);

            if(token == null)
            {
                throw new UnauthorizedAccessException();
            } else
            {
                if(!IsRefreshTokenExpired(token))
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    var newToken = GenerateJwtToken(token);
                    var tokenExpiration = DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"])).ToOffset(TimeSpan.FromHours(7));

                    token.AccessToken = newToken;

                    await _tokenRepository.UpdateAsync(token);

                    var result = new TokenReturnDto() 
                    {
                        accessToken = newToken,
                        tokenExpiration = tokenExpiration,
                    };

                    return result;
                }
            }
        }

        public async Task<int> RemoveRefreshTokenAsync(string assetToken)
        {
            var token = await _tokenRepository.GetRefreshTokenByAccessTokenAsync(assetToken);

            if(token == null)
            {
                throw new NotFoundException();
            } else
            {
                var result = await _tokenRepository.RemoveAsync(token);
                return result;
            }
        }

        /// <summary>
        /// Hàm kiểm tra xem refresh token còn hiệu lực hay không
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true nếu còn hiệu lực</returns>
        private static bool IsRefreshTokenExpired(Token token)
        {
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var refreshTokenExpirationTime = token.RefreshTokenExpiresAt.ToUnixTimeSeconds();

            return currentTime < refreshTokenExpirationTime;
        }

        
    }
}
