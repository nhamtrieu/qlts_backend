

namespace MISA.WebFresher062023.Demo.Domain
{
    public class AssetManager : IAssetManager
    {
        private readonly IAssetRepository _assetRepository;

        public AssetManager(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        /// <summary>
        /// hàm check trùng mã
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        public async Task CheckDuplicateCodeAsync(string code, Guid? id = null)
        {
            Asset? asset = null;
            asset = await _assetRepository.FindByCodeAsync(code);
            if (asset is not null)
            {
                throw new ConflictException(Resource.Resource.ConflictAssetCode);
            }
        }


        public void CheckLifeTimeAnDepreciationRate(int lifeTime, double depreciationRate)
        {
            var lifeTimeExpect = Math.Round((1 / (double)lifeTime), 4);
            var depreciationRateActual = Math.Round(depreciationRate / 100, 4);
            if (lifeTimeExpect != depreciationRateActual)
            {
                throw new ValidateException(Resource.Resource.EqualDepreciationRate);
            }
        }

        public void CheckPurchaseDateAndUseDate(DateTimeOffset purchaseDate, DateTimeOffset useDate)
        {
            if (purchaseDate > useDate)
            {
                throw new ValidateException(Resource.Resource.PurchaseDateLessUsedDate);
            }
        }

        public void CheckValidAssetCode(string code)
        {
            if (!char.IsDigit(code[code.Length - 1]))
            {
                throw new ValidateException(Resource.Resource.AssetCodeNotEndNumber);
            }
        }
    }
}
