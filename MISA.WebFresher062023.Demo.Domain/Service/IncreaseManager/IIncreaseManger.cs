namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IIncreaseManger : IEntityManager<Increase, Guid>
    {
        /// <summary>
        /// kiểm tra trùng mã
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task CheckDuplicateCodeAsync(string code, Guid? id=null);
        
        /// <summary>
        /// kiểm tra điều kiện mã thỏa mãn chưa 
        /// </summary>
        /// <param name="code"></param>
        void CheckValidIncreaseCode(string code);
    }
}
