using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;


namespace MISA.WebFresher062023.Demo
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// hàm để refresh token
        /// </summary>
        /// <returns>access token mới</returns>
        /// Createdby: TTNham (02/10/2023)
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            // lấy token từ header
            string oldAccessToken = HttpContext.Request.Headers["Authorization"];
            if(string.IsNullOrEmpty(oldAccessToken) || !oldAccessToken.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Vui lòng đăng nhập lại" });
            }

            // Loại bỏ "Bearer " để lấy giá trị access token.
            string tokenValue = oldAccessToken.Substring("Bearer ".Length);
            
            var newAccessToken = await _tokenService.RefreshTokenAsync(tokenValue);

            return Ok(newAccessToken);

        }
    }
}
