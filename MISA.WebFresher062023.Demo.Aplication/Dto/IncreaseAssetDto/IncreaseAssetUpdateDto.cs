namespace MISA.WebFresher062023.Demo.Application
{
    public class IncreaseAssetUpdateDto
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
        /// Id tài sản
        /// </summary>
        public Guid AssetId { get; set; }

        public float IncreaseCost { get; set; }

        public List<SourceCreateDto>? sourcesCreateDto { get; set; }

        public List<SourceUpdateDto>? sourcesUpdateDto { get; set; }

        public List<Guid>? sourcesDeleteId { get; set; }
    }
}
