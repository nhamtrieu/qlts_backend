using MISA.WebFresher062023.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher062023.Demo.Application
{
    public class SourceUpdateDto
    {
        /// <summary>
        /// Nguồn hình thành
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// giá trị
        /// </summary>
        public float Cost { get; set; }

        /// <summary>
        /// id tài sản ghi tăng
        /// </summary>
        public Guid IncreaseAssetId { get; set; }

        /// <summary>
        /// id nguôn hình thành
        /// </summary>
        public Guid SourceId { get; set; }
    }
}
