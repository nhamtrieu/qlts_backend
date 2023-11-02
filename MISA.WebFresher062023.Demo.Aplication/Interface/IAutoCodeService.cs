using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public interface IAutoCodeService
    {
        /// <summary>
        /// lấy mã mới
        /// </summary>
        /// <param name="codeType">loại mã</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        Task<string> GetNewCodeAsync(AutoCodeType codeType);

        /// <summary>
        /// cập nhật mã hiện tại
        /// </summary>
        /// <param name="codeType">loại mã</param>
        /// <param name="newCode">mã mới</param>
        /// <returns></returns>
        /// Createdby: ttnham (10/07/2023)
        Task<int> UpdateCodeAsync(AutoCodeType codeType, string newCode);
    }
}
