namespace MISA.WebFresher062023.Demo.Domain
{
    public class IncreaseAsset : IEntity<Guid>
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid IncreaseAssetId { get; set; }

        /// <summary>
        /// ID chứng từ
        /// </summary>
        public Guid IncreaseId { get; set; }

        /// <summary>
        /// id tài sản
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// nguyên giá của tài sản ghi tăng
        /// </summary>
        public float IncreaseCost { get; set; }

        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public Guid GetId()
        {
            return IncreaseAssetId;
        }

        public void SetCode(string code)
        {
            throw new NotImplementedException();
        }

        public void SetId(Guid id)
        {
            IncreaseAssetId = id;
        }
    }
}
