using MISA.WebFresher062023.Demo.Domain;
using OfficeOpenXml;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface IIncreaseService : ICrudService<Guid, IncreaseDto, IncreaseCreateDto, IncreasePutDto, Increase>
    {
        /// <summary>
        /// hàm phân trang và tìm kiếm 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Task<IncreaseFilterDto> FilterIncreaseAsync(int pageSize, int pageNumber, string? searchString);

        /// <summary>
        /// hàm xuất excel
        /// </summary>
        /// <param name="baseFilter">lọc theo điều kiện cơ bản</param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<ExcelPackage> ExportExcelAsync(FilterIncrease baseFilter);
    }
}
