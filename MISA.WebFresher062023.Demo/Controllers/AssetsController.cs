using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetsController : BaseCrudControler<Guid, AssetDto, AssetCreateDto, AssetUpdateDto, Asset>
    {
        private readonly IAssetService _assetService; 
        public AssetsController(IAssetService assetService) : base(assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// hàm lọc tài sản
        /// </summary>
        /// <param name="assetFilter"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        [HttpPost("Filter")]
        public async Task<IActionResult> AssetFilter([FromBody] FilterAssetCreateDto assetFilter)
        {
            var assetFilterDto = await _assetService.FilterAssetAsync(assetFilter);
            return StatusCode(StatusCodes.Status200OK, assetFilterDto);
        }

        /// <summary>
        /// hàm xuất excel
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] FilterAsset baseFilter)
        {
            var excelPackage = await _assetService.ExportExcelAsync(baseFilter);
            using (MemoryStream memoryStream = new ())
            {
                excelPackage.SaveAs(memoryStream);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_tai_san.xlsx");
            };
        }

        /// <summary>
        /// lấy chứng từ theo id tài sản
        /// </summary>
        /// <param name="id">id tài sản</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpGet("Increase/{id}")]

        public async Task<IActionResult> GetIncreaseByAssetId(Guid id)
        {
            var result = await _assetService.GetIncreaseByAssetIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// lấy chứng từ theo list id tài sản
        /// </summary>
        /// <param name="assetIds">list id tài sản</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpPost("Increase")]
        public async Task<IActionResult> GetIncreaseByListAssetIds ([FromBody] List<Guid> assetIds)
        {
            var result = await _assetService.GetIncreaseByListAssetIdsAsync(assetIds);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
