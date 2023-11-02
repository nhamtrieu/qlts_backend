namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IAutoCodeRepository
    {
        /// <summary>
        /// lấy mã hiện tại trong db
        /// </summary>
        /// <param name="codeType"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/10/2023)
        Task<AutoCode> GetMaxCodeAsync(AutoCodeType codeType);

        /// <summary>
        /// update mã trong db
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// Createdby: ttnham (10/10/2023)
        Task<int> UpdateMaxCodeAsync(AutoCode code);
    }
}
