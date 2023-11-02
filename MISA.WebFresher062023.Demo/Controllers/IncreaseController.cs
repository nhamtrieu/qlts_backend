using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class IncreaseController : BaseCrudControler<Guid, IncreaseDto, IncreaseCreateDto, IncreasePutDto, Increase>
    {
        private IIncreaseService _increaseService;
        public IncreaseController(IIncreaseService increaseService) : base(increaseService)
        {
            _increaseService = increaseService;
        }

        /// <summary>
        /// Lọc chứng từ theo điều kiện
        /// </summary>
        /// <param name="pageSize">số bản ghi trong 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="searchString">chuỗi tìm kiếm</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> GetIncreaseAsync(int pageSize, int pageNumber, string? searchString)
        {
            var result = await _increaseService.FilterIncreaseAsync(pageSize, pageNumber, searchString);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// cập nhật chứng từ
        /// </summary>
        /// <param name="increasePutDto">data update</param>
        /// <param name="id">id chứng từ</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutAsync(IncreasePutDto increasePutDto, Guid id)
        {
            var result = await _increaseService.UpdateAsync(id, increasePutDto);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] FilterIncrease filter)
        {
            var excelPackage = await _increaseService.ExportExcelAsync(filter);

            using (MemoryStream memoryStream = new())
            {
                excelPackage.SaveAs(memoryStream);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_chung_tu.xlsx");
            }
        }

    }
}
