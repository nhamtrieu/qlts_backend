using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SourceController : BaseCrudControler<Guid, SourceDto, SourceCreateDto, SourceUpdateDto, Source>
    {
        private readonly ISourceService _sourceService;
        
        public SourceController(ISourceService sourceService) : base(sourceService)
        {
            _sourceService = sourceService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllSourceAsync()
        //{
        //    var result = await _sourceService.GetAllAsync();

        //    return StatusCode(StatusCodes.Status200OK, result);
        //}

        /// <summary>
        /// Lấy nguồn hình thành theo id tài sản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpGet]
        [Route("asset-id/{id}")]
        public async Task<IActionResult> GetSourceByAssetIdAsync(Guid id)
        {
            var result = await _sourceService.GetSourceByAssetIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy nguồn hình thành theo id chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpGet]
        [Route("increase-id/{id}")]
        public async Task<IActionResult> GetSourceByIncreaseIdAsync(Guid id)
        {
            var result = await _sourceService.GetSourceByIncreaseIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy nguồn hình thành theo id tài sản ghi tăng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpGet]
        [Route("increase-asset-id/{id}")]
        public async Task<IActionResult> GetSourceByIncreaseAssetIdAsync(Guid id)
        {
            var result = await _sourceService.GetSourceByIncreaseAssetIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
