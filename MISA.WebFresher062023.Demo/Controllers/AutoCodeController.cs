using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AutoCodeController : ControllerBase
    {
        private readonly IAutoCodeService _autoCodeService;

        public AutoCodeController(IAutoCodeService autoCodeService)
        {
            _autoCodeService = autoCodeService;
        }

        /// <summary>
        /// Tạo mã mới
        /// </summary>
        /// <param name="codeType">loại mã tăng tự đông</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCodeAsync([FromQuery] AutoCodeType codeType)
        {
            var newCode = await _autoCodeService.GetNewCodeAsync(codeType);
            return StatusCode(StatusCodes.Status200OK, newCode);
        }
    }
}
