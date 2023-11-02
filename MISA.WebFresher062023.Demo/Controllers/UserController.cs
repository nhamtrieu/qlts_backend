using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;

namespace MISA.WebFresher062023.Demo
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Hàm đăng nhập
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        [HttpPost("login")]
        public async Task<IActionResult> CheckLogin([FromBody] UserDto userDto)
        {
            var result = await _userService.CheckUserLoginAsync(userDto);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// hàm xử lý đăng xuất
        /// </summary>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            string oldAccessToken = HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(oldAccessToken) || !oldAccessToken.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Vui lòng đăng nhập để thực hiện chức năng" });
            }

            // Loại bỏ "Bearer " để lấy giá trị access token.
            string tokenValue = oldAccessToken.Substring("Bearer ".Length);
            await _tokenService.RemoveRefreshTokenAsync(tokenValue);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
