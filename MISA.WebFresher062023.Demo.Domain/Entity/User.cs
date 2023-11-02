namespace MISA.WebFresher062023.Demo.Domain
{
    public class User : IEntity<Guid>
    {
        /// <summary>
        /// user id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// tên đăng nhập
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// họ và tên
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// mật khẩu đã đuọc mã hóa
        /// </summary>
        public string PasswordHash { get; set; }

        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public Guid GetId()
        {
            throw new NotImplementedException();
        }

        public void SetCode(string code)
        {
            throw new NotImplementedException();
        }

        public void SetId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
