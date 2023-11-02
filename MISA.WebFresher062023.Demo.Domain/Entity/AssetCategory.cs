using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher062023.Demo.Domain
{
    public class AssetCategory : BaseAuditEntity, IEntity<Guid>
    {
        /// <summary>
        /// ID loại tài sản
        /// </summary>
        [Required]
        public Guid AssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string? AssetCategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [MaxLength(50)]
        public string? AssetCategoryName { get; set; }


        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Thời gian sử dụng
        /// </summary>
        public int LifeTime { get; set; }


        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public Guid GetId()
        {
            return AssetCategoryId;
        }

        public void SetCode(string code)
        {
            throw new NotImplementedException();
        }

        public void SetId(Guid id)
        {
            AssetCategoryId = id;
        }
    }
}