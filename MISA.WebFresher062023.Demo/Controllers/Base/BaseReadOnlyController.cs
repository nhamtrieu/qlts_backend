using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controler]")]
    [ApiController]
    [Authorize]
    public abstract class BaseReadOnlyController<TKey, TEntityDto> : ControllerBase where TEntityDto : class
    {
        public readonly IReadOnlyService<TKey, TEntityDto> ReadOnlyService;

        protected BaseReadOnlyController(IReadOnlyService<TKey, TEntityDto> readOnlyService)
        {
            ReadOnlyService = readOnlyService;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: ttnham (18/08/23)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await ReadOnlyService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, result);

        }

        /// <summary>
        /// Lấy ra 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  Author: ttnham (18/08/23)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(TKey id)
        {
            var result = await ReadOnlyService.GetAsync(id);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
