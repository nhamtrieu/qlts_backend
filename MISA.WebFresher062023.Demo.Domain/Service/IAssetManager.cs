namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IAssetManager 
    {
        /// <summary>
        /// Hàm kiểm tra trùng mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// Createdby: ttnham (18/08/2023)
        Task CheckDuplicateCodeAsync(string code, Guid? id = null);

        /// <summary>
        /// Hàm kiểm tra điều kiện mã tải sản có hợp lệ không
        /// </summary>
        /// <param name="code"></param>
        void CheckValidAssetCode(string code);

        /// <summary>
        /// Hàm kiểm tra ngày mua và ngày sử dụng
        /// </summary>
        /// <param name="purchaseDate">Ngày mua</param>
        /// <param name="useDate">Ngày sử dụng</param>
        /// <returns></returns>
        /// Createdby: ttnham (07/09/2023)
        void CheckPurchaseDateAndUseDate(DateTimeOffset purchaseDate, DateTimeOffset useDate);

        /// <summary>
        /// Hàm kiểm tra thời gian sử dụng và tỷ lệ hao mòn
        /// </summary>
        /// <param name="lifeTime">Thời gian sử dụng</param>
        /// <param name="depreciationRate">Tỷ lệ hao mòn</param>
        /// <returns></returns>
        /// Createdby: ttnham (07/09/2023)
        void CheckLifeTimeAnDepreciationRate(int lifeTime, double depreciationRate);


    }
}
