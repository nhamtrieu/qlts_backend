namespace MISA.WebFresher062023.Demo.Application
{
    public interface IReadOnlyService<TKey,TEntityDto> where TEntityDto : class
    {
        /// <summary>
        /// hàm lấy tát cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<List<TEntityDto>> GetAllAsync();

        /// <summary>
        /// hàm lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<TEntityDto> GetAsync(TKey id);

        /// <summary>
        /// hàm lấy danh sách bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<List<TEntityDto>> GetManyAsync(List<TKey> ids);

        /// <summary>
        /// hàm lấy mã code mới
        /// </summary>
        /// <returns></returns>
        /// Createdby: ttnham (02/10/2023)
        Task<string> GetNewCode(string prefix);
    }
}
