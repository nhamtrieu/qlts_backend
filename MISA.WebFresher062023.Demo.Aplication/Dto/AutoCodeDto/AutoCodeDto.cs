using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class AutoCodeDto
    {
        /// <summary>
        ///  Tiền tố
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Loại mã
        /// </summary>
        public AutoCodeType CodeType { get; set; }
    }
}
