using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class IncreaseDetailController : BaseCrudControler<Guid, IncreaseAssetDto, IncreaseAssetCreateDto, IncreaseAssetUpdateDto, IncreaseAsset>
    {
        private readonly IIncreaseAssetService _increaseDetailService;
        #region contructer
        public IncreaseDetailController(IIncreaseAssetService increaseDetailService) : base(increaseDetailService)
        {
            _increaseDetailService = increaseDetailService;
        } 
        #endregion

        /// <summary>
        /// Lấy tài sản ghi tăng theo id chứng từ
        /// </summary>
        /// <param name="increaseId"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpGet]
        [Route("/increase-asset")]
        public async Task<IActionResult> GetIncreaseAssetByIncreaseId(Guid increaseId)
        {
            var result = await _increaseDetailService.GetIncreaseAssetByIncreaseId(increaseId);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut]
        [Route("{id}")]
        public override async Task<IActionResult> PutAsync(Guid id, [FromBody] IncreaseAssetUpdateDto entityUpdateDto)
        {
            var result = await _increaseDetailService.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
