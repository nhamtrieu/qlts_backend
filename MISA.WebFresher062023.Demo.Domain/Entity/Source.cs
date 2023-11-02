namespace MISA.WebFresher062023.Demo.Domain
{
    public class Source : IEntity<Guid>
    {
        /// <summary>
        /// id nguồn hình thành
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        /// nguồn hình thành
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// id tài sản ghi tăng
        /// </summary>
        public Guid IncreaseAssetId { get; set; }

        /// <summary>
        /// nguyên giá
        /// </summary>
        public float Cost { get; set; }

        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public Guid GetId()
        {
            return SourceId;
        }

        public void SetCode(string code)
        {
            throw new NotImplementedException();
        }

        public void SetId(Guid id)
        {
            SourceId = id;
        }
    }
}
