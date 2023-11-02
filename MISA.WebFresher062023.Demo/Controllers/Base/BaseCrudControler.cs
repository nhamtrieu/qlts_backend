using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher062023.Demo.Application;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseCrudControler<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> : BaseReadOnlyController<TKey, TEntityDto> where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class where TEntity : IEntity<TKey>
    {
        public readonly ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> CrudService;

        public BaseCrudControler(ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> crudOnlyService) : base(crudOnlyService)
        {
            CrudService = crudOnlyService;
        }


        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: ttnham (18/08/23)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            var result = await CrudService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }   

        /// <summary>
        /// Sửa 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        /// Author: ttnham (18/08/23)
        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(TKey id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {
            var result = await CrudService.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);

        }

        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: ttnham (18/08/2023)
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(TKey id)
        {
            var result = await CrudService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Hàm xóa nhiều tải sản
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// CreatedBy: ttnham (18/08/2023)
        [HttpDelete]
        public async Task<IActionResult> DeleteManyAsync(List<TKey> ids)
        {
            var result = await CrudService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status200OK, result);
        }


    }
}
