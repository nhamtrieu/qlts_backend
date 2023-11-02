using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using MISA.WebFresher062023.Demo.Domain.Resource;

namespace MISA.WebFresher062023.Demo.Application
{
    public class UserService : BaseReadOnlyService<User, Guid, UserDto>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly ITokenRepository _tokenRepository;
        public UserService(IMapper mapper, IUserRepository repository, IConfiguration config, ITokenRepository tokenRepository) : base(repository)
        {
            _mapper = mapper;
            _userRepository = repository;
            _config = config;
            _tokenRepository = tokenRepository;
        }

        public async Task<UserReturnDto> CheckUserLoginAsync(UserDto userDto)
        {
            var user = MapUserDtoToUser(userDto);
            var result = await _userRepository.CheckUserInfo(user);
            if (result == null)
            {
                throw new NotFoundException(message: Resource.IncorrectUsernameOrPassword, errorCode: 404);
            }
            else
            {
                if (result.PasswordHash == ComputeSha256Hash(userDto.Password))
                {
                    var tokenReturn = GenerateJwtToken(result.UserName);
                    var refreshTokenExpiration = DateTimeOffset.UtcNow.AddDays(Convert.ToDouble(_config["JwtSettings:RefreshTokenExpirationDays"])).ToOffset(TimeSpan.FromHours(7));

                    var tokenExpiration = DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"])).ToOffset(TimeSpan.FromHours(7));
                    var token = new Token()
                    {
                        AccessToken = tokenReturn,
                        RefreshTokenExpiresAt = refreshTokenExpiration,
                        RefreshTokenId = Guid.NewGuid(),
                        RefreshToken = Guid.NewGuid().ToString(),
                    };

                    var userReturn = new UserReturnDto()
                    {
                        token = tokenReturn,
                        tokenExpiration = tokenExpiration,
                    };

                    await _tokenRepository.AddAsync(token);

                    return  userReturn;
                }
                else
                {
                    throw new InvalidPasswordException(Resource.IncorrectUsernameOrPassword);
                }
            }
        }

        public string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserName", username),
                new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"])),
                signingCredentials: credentials
            );

            var userReturnDto = new UserReturnDto()
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                tokenExpiration = DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationMinutes"])).ToOffset(TimeSpan.FromHours(7))
            };

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }

        private static string ComputeSha256Hash(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            StringBuilder buider = new();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                buider.Append(hashBytes[i].ToString("x2"));
            }
            return buider.ToString();
        }


        public override UserDto MapEntityToEntityDto(User entity)
        {
            throw new NotImplementedException();
        }

        public User MapUserDtoToUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return user;

        }

        public UserDto MapUserToUserDto(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }


        public override string IncrementNumberString(string numberString)
        {
            throw new NotImplementedException();
        }
    }
}
