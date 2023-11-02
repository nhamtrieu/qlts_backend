namespace MISA.WebFresher062023.Demo.Domain
{
    public class AutoCode
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
